package com.example.einmobiliaria.ui

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.Menu
import android.view.MenuItem
import android.widget.SearchView
import androidx.recyclerview.widget.RecyclerView
import com.example.einmobiliaria.MainActivity
import com.example.einmobiliaria.R
import com.example.einmobiliaria.repository.RepositorySingleton
import com.example.einmobiliaria.adapters.AdapterChecks
import com.example.einmobiliaria.models.out.CheckDetailInfoModel

class AparmentsInCheck : AppCompatActivity() , SearchView.OnQueryTextListener  {

    private lateinit var search : SearchView
    private lateinit var recycler : RecyclerView
    private lateinit var adapter: AdapterChecks
    private lateinit var role : String
    lateinit var  bundle : Bundle

    override fun onCreate(savedInstanceState: Bundle?) {

        super.onCreate(savedInstanceState)

        setContentView(R.layout.activity_aparments_in_check)

        initViews()

        // agrego botón para ir hacia atrás
        supportActionBar!!.setDisplayHomeAsUpEnabled(true)
    }

    private fun initViews() {
        search = findViewById(R.id.searchViewApartSaved)
        search.setOnQueryTextListener(this)
        recycler = findViewById(R.id.RecyclerMyAparmentsSaved)
    }

    override fun onQueryTextSubmit(filterString: String?): Boolean {
        adapter!!.filter.filter(filterString)
        return true
    }

    override fun onQueryTextChange(filterString: String?): Boolean {
        adapter!!.filter.filter(filterString)
        return true
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
                val intent = Intent(this, DashboardChecker::class.java)
                startActivity(intent)
                true
            }
            else -> return super.onOptionsItemSelected(item)
        }
    }

    override fun onResume() {

        super.onResume()

        val myId = RepositorySingleton.user?.Id
        RepositorySingleton.aparmentsInCheck = RepositorySingleton.checks.filter { s -> (s.State == "DOING" && s.UserId == myId) } as ArrayList<CheckDetailInfoModel>
        val myApartmentsInCheck = RepositorySingleton.aparmentsInCheck
        adapter = AdapterChecks()
        adapter.setData(myApartmentsInCheck ,false, "DOING")
        recycler.adapter = adapter

        role =  "Checker"
    }
}