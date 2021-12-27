package com.example.einmobiliaria.ui

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.view.Menu
import android.view.MenuItem
import androidx.appcompat.app.AppCompatActivity
import androidx.cardview.widget.CardView
import com.example.einmobiliaria.CreateApartment
import com.example.einmobiliaria.MainActivity
import com.example.einmobiliaria.R
import com.example.einmobiliaria.repository.RepositorySingleton
import com.example.einmobiliaria.models.out.CheckDetailInfoModel
import com.example.einmobiliaria.services.RestApiService


class DashboardAdmin : AppCompatActivity() {
    private lateinit var bundle : Bundle
    private lateinit var cardEditProfile : CardView
    private lateinit var cardApartmentsRegistered : CardView
    private lateinit var cardApartmentsToCheck : CardView
    private  lateinit var cardAparmentsChecked : CardView
    private lateinit var cardAddAparment : CardView
    private lateinit var role : String
    private lateinit var apiService : RestApiService

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_dashboard_admin)

        initViews()
        role = RepositorySingleton.user?.Role.toString()
        apiService = RestApiService()

        cardEditProfile.setOnClickListener {
            intent = Intent(this, EditProfile::class.java)
            bundle = Bundle()
            bundle.putString("role", role)
            intent.putExtras(bundle)
            startActivity(intent)
        }

        cardAddAparment.setOnClickListener { view ->

            intent = Intent(this, CreateApartment::class.java)
            startActivity(intent)
        }

        cardApartmentsToCheck.setOnClickListener { view ->
            val intent = Intent(this, UncheckAparments::class.java)
            startActivity(intent)
        }

        cardApartmentsRegistered.setOnClickListener { view ->
            val intent = Intent(this, ApartmentsRegistered::class.java)
            startActivity(intent)

        }
        cardAparmentsChecked.setOnClickListener { view ->
            val intent = Intent(this, AparmentsChecked::class.java)
            startActivity(intent)
        }

    }

    private fun initViews() {
        cardEditProfile = findViewById(R.id.cardEditProfile)
        cardApartmentsRegistered = findViewById(R.id.cardApartmentsRegistered)
        cardApartmentsToCheck = findViewById(R.id.cardApartmentsToCheck)
        cardAparmentsChecked = findViewById(R.id.cardAparmentsChecked)
        cardAddAparment = findViewById(R.id.cardAddAparment)
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
                RepositorySingleton.token = ""
                val intent = Intent(this, MainActivity::class.java)
                startActivity(intent)
                true
            }
           else -> return super.onOptionsItemSelected(item)
       }
    }

    override fun onResume() {

        super.onResume()
        apiService.getRents{ s, rents ->
        }
        apiService.getAllApartments{ s, apartments ->

        }
        apiService.getAllChecks{ s,  checks ->

        }
    }

}