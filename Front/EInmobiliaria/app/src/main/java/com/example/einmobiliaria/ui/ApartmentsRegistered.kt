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
import com.example.einmobiliaria.adapters.AdapterAparments
import com.example.einmobiliaria.models.out.ApartmentBasicInfoModel


class ApartmentsRegistered : AppCompatActivity() , SearchView.OnQueryTextListener  {


    private lateinit var search : SearchView
    private lateinit var recycler : RecyclerView
    private lateinit var aparments: ArrayList<ApartmentBasicInfoModel>
    private lateinit var adapter: AdapterAparments
    private lateinit var role : String
    lateinit var  bundle : Bundle


    override fun onCreate(savedInstanceState: Bundle?) {

        super.onCreate(savedInstanceState)

        setContentView(R.layout.activity_apartments_registered)

        search = findViewById(R.id.searchView)
        search.setOnQueryTextListener(this)
        recycler = findViewById(R.id.RecyclerAparments)
        // agrego botón para ir hacia atrás
        supportActionBar!!.setDisplayHomeAsUpEnabled(true)

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
                val intent = Intent(this, DashboardAdmin::class.java)
                startActivity(intent)
                true
            }
            else -> return super.onOptionsItemSelected(item)
        }
    }

    override fun onResume() {

        super.onResume()
        var userId = RepositorySingleton.user?.Id
        RepositorySingleton.aparmentsRegistered = RepositorySingleton.aparments.filter { s -> s.State == "REGISTERED"} as ArrayList<ApartmentBasicInfoModel>
        aparments = RepositorySingleton.aparmentsRegistered!!
        adapter = AdapterAparments()
        adapter.setData(aparments , userId!!)
        recycler.adapter = adapter
        role =  "Admin"
    }

}
