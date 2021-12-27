package com.example.einmobiliaria.ui

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.*
import androidx.appcompat.app.AppCompatActivity
import com.example.einmobiliaria.R
import com.example.einmobiliaria.Register
import com.example.einmobiliaria.repository.RepositorySingleton
import com.example.einmobiliaria.models.RoleEnum
import com.example.einmobiliaria.models.`in`.SessionModel
import com.example.einmobiliaria.services.RestApiService


class LogIn : AppCompatActivity() , AdapterView.OnItemSelectedListener {

    lateinit var email : EditText
    lateinit var password : EditText
    lateinit var bundle : Bundle
    lateinit var user : String


    override fun onCreate(savedInstanceState: Bundle?) {

        super.onCreate(savedInstanceState)

        setContentView(R.layout.activity_log_in)

        email = findViewById(R.id.textEmailAddress)

        password = findViewById(R.id.textPassword)


        val buttonLogIn = findViewById<Button>(R.id.logInButton)
        buttonLogIn.setOnClickListener{
            if (isValidEmailAndPassword()) {
                val sessionModel = SessionModel(email.text.toString(),password.text.toString())

                val apiService = RestApiService()
                apiService.logIn(sessionModel){
                    if(it==""){
                        var intent : Intent
                        apiService.getUsersByEmail(sessionModel.Email){ userBasicInfoModel ->
                            if(userBasicInfoModel!=null){
                                RepositorySingleton.user = userBasicInfoModel

                                val role = userBasicInfoModel.Role
                                when (role) {
                                    RoleEnum.ADMIN -> {
                                        intent = Intent(this, DashboardAdmin::class.java)
                                        user = email.text.toString()
                                        bundle = Bundle()
                                        bundle.putString("user",user)
                                        intent.putExtras(bundle)
                                        startActivity(intent)
                                        overridePendingTransition(android.R.anim.slide_in_left,android.R.anim.slide_out_right)
                                    }
                                    RoleEnum.CHECKER -> {
                                        intent = Intent(this, DashboardChecker::class.java)
                                        user = email.text.toString()
                                        bundle = Bundle()
                                        bundle.putString("user",user)
                                        intent.putExtras(bundle)
                                        startActivity(intent)
                                        overridePendingTransition(android.R.anim.slide_in_left,android.R.anim.slide_out_right)
                                    }
                                    else -> {
                                        Toast.makeText(this@LogIn,"Debe elegir un role", Toast.LENGTH_LONG).show()
                                    }
                                }

                            }
                        }
                    }
                    else {
                        Toast.makeText(this@LogIn, it.toString(), Toast.LENGTH_LONG).show()
                    }
                }

            }
            else {
                Toast.makeText(this@LogIn, "El email y/o contraseña no pueden ser vacíos", Toast.LENGTH_LONG).show()
            }

        }

        val buttonRegister = findViewById<Button>(R.id.registerButton)
        buttonRegister.setOnClickListener{
            val intent = Intent(this, Register::class.java)
            startActivity(intent)
        }

    }

    override fun onItemSelected(parent: AdapterView<*>?, view: View?, position: Int, id: Long) {
        Log.d("Spinner", "onItemSelected")
    }

    override fun onNothingSelected(parent: AdapterView<*>?) {
        Log.d("Spinner", "onNothingSelected")
    }

    private fun isValidEmailAndPassword () : Boolean {

        return !email.text.isNullOrEmpty() &&
                !password.text.isNullOrEmpty()
    }

}
