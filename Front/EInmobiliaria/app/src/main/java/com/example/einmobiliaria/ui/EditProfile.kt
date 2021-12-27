package com.example.einmobiliaria.ui

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.Menu
import android.view.MenuItem
import android.view.View
import android.widget.Button
import android.widget.Toast
import com.example.einmobiliaria.MainActivity
import com.example.einmobiliaria.R
import com.example.einmobiliaria.repository.RepositorySingleton
import com.example.einmobiliaria.models.`in`.UserModel
import com.example.einmobiliaria.services.RestApiService
import com.google.android.material.textfield.TextInputEditText

class EditProfile : AppCompatActivity() {
    lateinit var role : String
    lateinit var bundle: Bundle
    private  var userId : Int = 0

    override fun onCreate(savedInstanceState: Bundle?) {

        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_edit_profile)

        val user = RepositorySingleton.user

        bundle = intent.extras!!
        role = user?.Role.toString()
        userId = user?.Id!!

        val editProfile = findViewById<View>(R.id.buttonEditProfile) as Button


        val name = findViewById<TextInputEditText>(R.id.textInputEditTextNameProfile)
        val email = findViewById<TextInputEditText>(R.id.textInputEditTextEmailProfile)
        val pass = findViewById<TextInputEditText>(R.id.textInputEditTextPassProfile)

        name.setText(user?.Name)
        email.setText(user?.Email)

        editProfile.setOnClickListener {
            var userModel = UserModel(name.text.toString(), email.text.toString(),"", role.toUpperCase())
            if (pass.text.toString()!= ""){
                userModel.Password = pass.text.toString()
            }
            val apiService = RestApiService()
            if (user != null) {
                apiService.editUser(userId, userModel){ s,result ->
                    if(result!=null){
                        Toast.makeText(this@EditProfile,"Se han editado sus datos",Toast.LENGTH_LONG).show()
                        var intent : Intent
                        when (role) {
                            "Admin" -> {
                                intent = Intent(this, DashboardAdmin::class.java)
                                startActivity(intent)
                            }
                            "Checker" -> {
                                intent = Intent(this, DashboardChecker::class.java)
                                startActivity(intent)
                            }
                        }
                    } else {
                        Toast.makeText(this@EditProfile,s,Toast.LENGTH_LONG).show()
                    }
                }
            }

        }
        // agrego botón para ir hacia atrás
        supportActionBar!!.setDisplayHomeAsUpEnabled(true)

    }
    // se crea el menú
    override fun onCreateOptionsMenu(menu: Menu?): Boolean {
        menuInflater.inflate(R.menu.menu_logout, menu)
        return true
    }

    // se determina que se hace al seleccionar un item del menú
    override fun onOptionsItemSelected(item: MenuItem): Boolean {
        return when (item.itemId ) {
            // si se apreta el botón para ir para atras
            android.R.id.home -> {
                onBackPressed()
                true
            }
            R.id.nav_logout -> {
                RepositorySingleton.user  = null
                RepositorySingleton.token = ""
                val intent = Intent(this, MainActivity::class.java)
                startActivity(intent)
                true  }
            else -> return super.onOptionsItemSelected(item)
        }
    }
}