package com.example.einmobiliaria

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.Menu
import android.view.MenuItem
import android.widget.*
import com.example.einmobiliaria.models.`in`.ApartmentModel
import com.example.einmobiliaria.models.out.UserBasicInfoModel
import com.example.einmobiliaria.repository.RepositorySingleton
import com.example.einmobiliaria.services.RestApiService
import com.example.einmobiliaria.ui.*

class CreateApartment : AppCompatActivity() {
    private lateinit var btnAddAparment : Button
    private lateinit var  nameApartment : EditText
    private lateinit var  descriptionApartment : EditText
    private lateinit var  idAparment : String
    private lateinit var imgUbicarme : ImageView
    private lateinit var latitud: String
    private lateinit var longitud : String
    private lateinit var  ubicacion : EditText
    private lateinit var ubicacionLatYLong : String
    private lateinit var bundle: Bundle
    private  var latitudDouble : Double  ? = null
    private  var longitudDouble : Double ?  = null
    private lateinit var role : String
    private  var name : String ? = null
    private  var description : String ? = null


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_create_apartment)
        initView()
        role =  "Admin"
        latitudDouble = intent.getDoubleExtra("latitud", 0.0);
        longitudDouble = intent.getDoubleExtra("longitud", 0.0);
        latitud = latitudDouble.toString()
        longitud= longitudDouble.toString()

        name = intent.getStringExtra("nameApto");
        if(name != null){
            nameApartment.setText(name)

        }
        description = intent.getStringExtra("descriptionApto")
        if(description != null){

            descriptionApartment.setText(description)
        }

        if (latitudDouble!= 0.0 || longitudDouble!= 0.0 ) {

            ubicacionLatYLong =" Ubicación Agregada"
            ubicacion.setText(ubicacionLatYLong)

        }

        imgUbicarme.setOnClickListener { view ->

            intent = Intent(this, MapsActivity::class.java)
            var bundle = Bundle()
            bundle.putString("nameApto",nameApartment.text.toString())
            bundle.putString("descriptionApto",descriptionApartment.text.toString())
            intent.putExtras(bundle)

            startActivity(intent)
        }
        btnAddAparment.setOnClickListener { view ->

            val newAparment =  ApartmentModel( nameApartment.text.toString(), descriptionApartment.text.toString(),
            latitudDouble!!.toString(), longitudDouble!!.toString())

            val apiService = RestApiService()

            apiService.createAparment(newAparment){}

            Toast.makeText(
                    this@CreateApartment,
                    "Apartamento ${nameApartment.text.toString()} " +
                           "registrado con éxito",
                    Toast.LENGTH_LONG
            ).show()
            // actualizo lista de apartamentos con el nuevo apartamento creado
            apiService.getAllApartments{ s, arrayList ->

            }
            val intent = Intent(this, DashboardAdmin::class.java)
            startActivity(intent)
        }

        // agrego botón para ir hacia atrás
        supportActionBar!!.setDisplayHomeAsUpEnabled(true)

    }

    private fun initView() {
        btnAddAparment = findViewById(R.id.buttonAddAparment)
        nameApartment = findViewById(R.id.textNameApartment)
        imgUbicarme =  findViewById(R.id.imgUbicarme)
        ubicacion = findViewById(R.id.ubicacion)
        descriptionApartment =  findViewById(R.id.textDescription)
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
}