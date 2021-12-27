package com.example.einmobiliaria.ui

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.Menu
import android.view.MenuItem
import android.widget.Toast
import androidx.recyclerview.widget.RecyclerView
import com.example.einmobiliaria.MainActivity
import com.example.einmobiliaria.R
import com.example.einmobiliaria.repository.RepositorySingleton
import com.example.einmobiliaria.adapters.AdapterUnCheckAparments
import com.example.einmobiliaria.adapters.OnAparmentUncheckClickListener
import com.example.einmobiliaria.models.`in`.CheckModel
import com.example.einmobiliaria.services.RestApiService
import com.google.android.material.floatingactionbutton.FloatingActionButton

class UncheckAparments : AppCompatActivity() , OnAparmentUncheckClickListener {

    private lateinit var btnNotify : FloatingActionButton
    private lateinit var recycler : RecyclerView
    private lateinit var aparments: List<String>
    private lateinit var role : String
    private  var userId : Int = 0
    lateinit var  bundle : Bundle

    override fun onCreate(savedInstanceState: Bundle?) {

        super.onCreate(savedInstanceState)

        setContentView(R.layout.activity_uncheck_aparments)

        initViews ()

        btnNotify.setOnClickListener(){
            val checkModel = CheckModel(getIdRents(),getIdApartments(),userId)
            val apiService = RestApiService()
            apiService.notify(checkModel){ s , checks ->
                if (checks!=null){
                    Toast.makeText(this, "Se ha notificado a los usuarios chequeadores", Toast.LENGTH_SHORT).show()
                    RepositorySingleton.aparmentsToNotify = ArrayList()
                    val intent = Intent(this, DashboardAdmin::class.java)
                    startActivity(intent)
                } else {
                    Toast.makeText(this, s, Toast.LENGTH_SHORT).show()
                }
            }

            //actualizo lista de checks
            apiService.getAllChecks(){ s, arrayList ->
                if (arrayList!=null){
                    RepositorySingleton.checks.clear()
                    RepositorySingleton.checks.addAll(arrayList)
                }
            }
            // actualizo la lista de aptos
            apiService.getAllApartments{ s, arrayList ->

            }

        }

        // agrego botón para ir hacia atrás
        supportActionBar!!.setDisplayHomeAsUpEnabled(true)
    }


    fun initViews (){
        recycler = findViewById(R.id.RecyclerUnchekedAparments)
        btnNotify = findViewById(R.id.floatingButtonNotify)
    }

    override fun onAparmentClick(element: String, position: Int){

        Toast.makeText(this, element, Toast.LENGTH_SHORT).show()
    }

    // se crea el menú
    override fun onCreateOptionsMenu(menu: Menu?): Boolean {
        menuInflater.inflate(R.menu.main_menu, menu)
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
            R.id.nav_profile -> {
                val intent = Intent(this, EditProfile::class.java)
                var bundle = Bundle()
                bundle.putString("role",role)
                intent.putExtras(bundle)
                startActivity(intent)
                true
            }
            R.id.nav_dashboard -> {
                val intent = Intent(this, DashboardAdmin::class.java)
                startActivity(intent)
                true
            }
            else -> return super.onOptionsItemSelected(item)
        }
    }

    fun getNamesApartments() : List<String>{
        val rents = RepositorySingleton.aparmentsToNotify
        var listNames  : List<String> = emptyList()
        if (rents.count() == 0){
            Toast.makeText(this, "No hay apartamentos a ser notificados", Toast.LENGTH_SHORT).show()
            val intent = Intent(this, DashboardAdmin::class.java)
            startActivity(intent)
        } else {
            val result : List<String> = rents.map { x -> x.Apartment.Name }
            listNames = result.distinct()
        }
        return listNames
    }

    fun getIdApartments() : List<Integer>{

        val rents = RepositorySingleton.aparmentsToNotify

        var listNames  : List<Integer> = emptyList()
        if (rents.count() == 0){
            Toast.makeText(this, "No hay apartamentos a ser notificados", Toast.LENGTH_SHORT).show()
            val intent = Intent(this, DashboardAdmin::class.java)
            startActivity(intent)
        } else {
            val result : List<Integer> = rents.map { x -> x.ApartmentId }
            listNames = result.distinct()
        }
        return listNames
    }

    fun getIdRents() : List<Integer>{
        val rents = RepositorySingleton.aparmentsToNotify

        var listNames  : List<Integer> = emptyList()

        if (rents.count() == 0){
            Toast.makeText(this, "No hay apartamentos a ser notificados", Toast.LENGTH_SHORT).show()
            val intent = Intent(this, DashboardAdmin::class.java)
            startActivity(intent)
        } else {
            val result : List<Integer> = rents.map { x -> x.Id }
            listNames = result
        }
        return listNames
    }

    override fun onResume() {

        super.onResume()
        role =  RepositorySingleton.user?.Role.toString()
        userId = RepositorySingleton.user?.Id!!
        aparments = ArrayList<String>()
        aparments  = getNamesApartments()
        recycler.adapter = AdapterUnCheckAparments(aparments!!, this)


    }
}