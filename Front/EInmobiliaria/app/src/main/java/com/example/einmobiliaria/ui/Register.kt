package com.example.einmobiliaria

import android.content.Intent
import android.os.Bundle
import android.util.Patterns
import android.view.View
import android.widget.*
import androidx.appcompat.app.AppCompatActivity
import com.example.einmobiliaria.models.RoleEnum.*
import com.example.einmobiliaria.models.`in`.SessionModel
import com.example.einmobiliaria.models.`in`.UserModel
import com.example.einmobiliaria.repository.RepositorySingleton
import com.example.einmobiliaria.services.RestApiService
import com.example.einmobiliaria.ui.*
import java.util.regex.Pattern

class Register : AppCompatActivity(), AdapterView.OnItemSelectedListener {

    private lateinit var btnRegister: Button
    private lateinit var spinner: Spinner
    private lateinit var name : EditText
    private lateinit var email : EditText
    private lateinit var password : EditText
    private lateinit var confirmPassword : EditText
    private lateinit var adapterSppiner : ArrayAdapter<String>
    private lateinit var roles : Array<String>
    private lateinit var role : String
    private lateinit var bundle : Bundle
    private lateinit var user : String


    override fun onCreate(savedInstanceState: Bundle?) {

        super.onCreate(savedInstanceState)

        setContentView(R.layout.activity_register)

        btnRegister = findViewById(R.id.registerButton)

        spinner = findViewById(R.id.spinnerRole)

        name = findViewById(R.id.textNameElement)

        email = findViewById(R.id.textEmailAddress)

        password = findViewById(R.id.textPassword)

        confirmPassword = findViewById(R.id.textConfirmPassword)

        spinner = findViewById(R.id.spinnerRole)

        spinner.setOnItemSelectedListener(this)

        roles = resources.getStringArray(R.array.Roles)

        adapterSppiner = ArrayAdapter(this, android.R.layout.simple_spinner_item, roles)

        spinner.adapter = adapterSppiner
        val adapter = ArrayAdapter(this, R.layout.color_spinner_layout, roles)
        adapter.setDropDownViewResource(R.layout.color_spinner_dropdown_layout)
        spinner.adapter = adapter

        val indice: Int = spinner.selectedItemPosition

        if (indice == 0) {
            role = "Admin"
        } else {
            role = "Checker"
        }
        if (spinner != null) {
            val adapter = ArrayAdapter(this, R.layout.color_spinner_layout, roles)
            adapter.setDropDownViewResource(R.layout.color_spinner_dropdown_layout)
            spinner.adapter = adapter
            spinner.onItemSelectedListener = object :
                AdapterView.OnItemSelectedListener {
                override fun onItemSelected(
                    parent: AdapterView<*>,
                    view: View,
                    position: Int,
                    id: Long
                ) {
                    role = roles[position].toString()
                }

                override fun onNothingSelected(parent: AdapterView<*>) {
                    role = roles[0].toString()
                }
            }
        }



        btnRegister.setOnClickListener {

            if (isValidEmailAndPassword () && isValidEmail(email.text.toString()) && isValidPassword(
                    password.text.toString()
                )) {

                val userModel = UserModel(
                    name.text.toString(),
                    email.text.toString(),
                    password.text.toString(),
                    role.toUpperCase()
                )
                val apiService = RestApiService()
                apiService.register(userModel) { it ->
                    if (it != null) {
                        Toast.makeText(this@Register, "Registro exitoso", Toast.LENGTH_LONG).show()
                        val sessionModel = SessionModel(
                            email.text.toString(),
                            password.text.toString()
                        )
                        apiService.getUsersByEmail(sessionModel.Email){ userBasicModel ->
                            if(userBasicModel!=null){
                                RepositorySingleton.user = userBasicModel
                                val roleUser = userBasicModel.Role
                                apiService.logIn(sessionModel) { it2 ->
                                    if (it2 == "") {
                                        var intent: Intent
                                        when (roleUser) {
                                            ADMIN -> {
                                                intent = Intent(this, DashboardAdmin::class.java)
                                                user = email.text.toString()
                                                bundle = Bundle()
                                                bundle.putString("user", user)
                                                intent.putExtras(bundle)
                                                startActivity(intent)
                                                overridePendingTransition(
                                                    android.R.anim.slide_in_left,
                                                    android.R.anim.slide_out_right
                                                )

                                            }
                                            CHECKER -> {
                                                intent = Intent(this, DashboardChecker::class.java)
                                                user = email.text.toString()
                                                bundle = Bundle()
                                                bundle.putString("user", user)
                                                intent.putExtras(bundle)
                                                startActivity(intent)
                                                overridePendingTransition(
                                                    android.R.anim.slide_in_left,
                                                    android.R.anim.slide_out_right
                                                )
                                            }
                                            else -> {
                                                Toast.makeText(
                                                    this@Register,
                                                    "Debe elegir un rol",
                                                    Toast.LENGTH_LONG
                                                ).show()
                                            }
                                        }
                                    } else {
                                        Toast.makeText(
                                            this@Register,
                                            "No logro iniciar sesion",
                                            Toast.LENGTH_LONG
                                        ).show()
                                    }
                                }
                            }
                        }

                    } else {
                        Toast.makeText(
                            this@Register,
                            "No se logro registrar correctamente, trate de logguearse con dicho mail",
                            Toast.LENGTH_LONG
                        ).show()
                    }
                }

            }
            else  // !isValidEmailOrPass
            {
                if (!isValidEmail(email.text.toString())){
                    Toast.makeText(
                        this@Register,
                        "Ingrese un email válido", Toast.LENGTH_LONG
                    ).show()
                }
                if (!isValidPassword(password.text.toString())){
                    Toast.makeText(
                        this@Register,
                        "La contraseña debe contener mínimo seis caracteres, al menos una letra y un número",
                        Toast.LENGTH_LONG
                    ).show()
                }
                if (email.text.isNullOrEmpty() ||
                    password.text.isNullOrEmpty() ||
                    confirmPassword.text.isNullOrEmpty() ||
                    name.text.isNullOrEmpty()) {
                        Toast.makeText(
                            this@Register,
                            "Los campos no puede ser vacío", Toast.LENGTH_LONG
                        ).show()
                }
                else if (password.text.toString() != confirmPassword.text.toString()){
                        Toast.makeText(
                            this@Register,
                            "Las contraseñas deben coincidir",
                            Toast.LENGTH_LONG
                        ).show()
                    }
                }
            }

    }


    override fun onItemSelected(parent: AdapterView<*>?, view: View?, position: Int, id: Long) {

    }

    override fun onNothingSelected(parent: AdapterView<*>?) {

    }

    private fun isValidEmailAndPassword () : Boolean {

        return !email.text.isNullOrEmpty() &&
                !password.text.isNullOrEmpty() &&
                !confirmPassword.text.isNullOrEmpty() &&
                !name.text.isNullOrEmpty() &&
                password.text.toString() == confirmPassword.text.toString()
    }

    private fun isValidEmail(email: String) : Boolean {
        val pattern = Patterns.EMAIL_ADDRESS
        return pattern.matcher(email).matches()
    }
    private fun isValidPassword(passdowrd: String) : Boolean {

        //Mínimo seis caracteres, al menos una letra y un número
        val passwordPatter = "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{6,}$"
        val pattern = Pattern.compile(passwordPatter)
        return pattern.matcher(passdowrd).matches()

    }

}