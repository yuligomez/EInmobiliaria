package com.example.einmobiliaria.ui

import android.annotation.SuppressLint
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.Menu
import android.view.MenuItem
import androidx.cardview.widget.CardView
import com.example.einmobiliaria.R
import com.example.einmobiliaria.repository.RepositorySingleton
import com.example.einmobiliaria.models.out.CheckDetailInfoModel
import com.example.einmobiliaria.services.RestApiService
import kotlin.properties.Delegates

class DashboardChecker : AppCompatActivity() {
    lateinit var cardEditProfile : CardView
    lateinit var cardApartmentsToCheck : CardView
    lateinit var cardMyApartmentsChecked : CardView
    lateinit var cardMyApartmentsSaved : CardView
    private lateinit var apiService : RestApiService
    lateinit var role : String
    lateinit var bundle: Bundle
    var userId : Int =0

    @SuppressLint("RestrictedApi")
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_dashboard_checker)

        initViews()
        role = RepositorySingleton.user?.Role.toString()



        cardEditProfile.setOnClickListener { view ->
            intent = Intent(this, EditProfile::class.java)
            bundle = Bundle()
            bundle.putString("role",role)
            intent.putExtras(bundle)
            startActivity(intent)
        }

        cardApartmentsToCheck.setOnClickListener { view ->
            val intent = Intent(this, AparmentsToCheck::class.java)
            startActivity(intent)
        }
        cardMyApartmentsChecked.setOnClickListener { view ->
            val intent = Intent(this, AparmentsChecked::class.java)
            startActivity(intent)
        }
        cardMyApartmentsSaved.setOnClickListener { view ->
            val intent = Intent(this, AparmentsInCheck::class.java)
            startActivity(intent)
        }

    }
    private fun initViews() {

        cardEditProfile = findViewById(R.id.cardEditProfileChecker)
        cardApartmentsToCheck = findViewById(R.id.cardApartmentsToCheckChecker)
        cardMyApartmentsChecked = findViewById(R.id.cardMyApartmentsChecked)
        cardMyApartmentsSaved = findViewById(R.id.cardMyApartmentsSaved)

    }

    // se crea el menú
    override fun onCreateOptionsMenu(menu: Menu?): Boolean {
        menuInflater.inflate(R.menu.menu_logout, menu)
        return true
    }

    // se determina que se hace al seleccionar un item del menú
    override fun onOptionsItemSelected(item: MenuItem): Boolean {
        return when (item.itemId ) {

            R.id.nav_logout -> {
                RepositorySingleton.user  = null
                finish()
                true
            }
            else -> return super.onOptionsItemSelected(item)
        }
    }
    override fun onResume() {

        super.onResume()
        apiService = RestApiService()
        apiService.getRents{ s, rents ->
        }
        apiService.getAllApartments{ s, apartments ->

        }
        apiService.getAllChecks{ s,  checks ->

        }
    }
}