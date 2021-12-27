package com.example.einmobiliaria.ui

import android.content.Intent
import android.os.Bundle
import android.view.Menu
import android.view.MenuItem
import android.widget.SearchView
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.RecyclerView
import com.example.einmobiliaria.MainActivity
import com.example.einmobiliaria.R
import com.example.einmobiliaria.repository.RepositorySingleton
import com.example.einmobiliaria.adapters.AdapterChecks
import com.example.einmobiliaria.models.RoleEnum
import com.example.einmobiliaria.models.out.CheckDetailInfoModel
import com.example.einmobiliaria.services.RestApiService


class AparmentsChecked : AppCompatActivity() , SearchView.OnQueryTextListener  {

    private lateinit var search : SearchView
    private lateinit var recycler : RecyclerView
    private lateinit var adapter: AdapterChecks
    private lateinit var apiService : RestApiService
    private  var isApartmentChecked : Boolean = false

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_aparments_checked)
        initViews()


        // agrego botón para ir hacia atrás
        supportActionBar!!.setDisplayHomeAsUpEnabled(true)
    }

    private fun initViews() {
        search = findViewById(R.id.searchViewApartChecked)
        search.setOnQueryTextListener(this)
        recycler = findViewById(R.id.RecyclerMyAparmentsChecked)

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
                true
            }
            else -> return super.onOptionsItemSelected(item)
        }
    }

    override fun onResume() {
        super.onResume()
        apiService = RestApiService()
        RepositorySingleton.aparmentsChecked = RepositorySingleton.checks.filter { s -> s.State == "DONE"  } as ArrayList<CheckDetailInfoModel>
        var apartmentsChecked = ArrayList<CheckDetailInfoModel> ()
        adapter = AdapterChecks()

        if (RepositorySingleton.user?.Role.toString() == "CHECKER"){

            apartmentsChecked = RepositorySingleton.aparmentsChecked.filter { s -> s.State == "DONE" && s.UserId == RepositorySingleton.user
                    ?.Id} as ArrayList<CheckDetailInfoModel>
        }
        else {
            apartmentsChecked = RepositorySingleton.aparmentsChecked
        }
        adapter.setData(apartmentsChecked, true, "DONE")
        recycler.adapter = adapter
    }
}



