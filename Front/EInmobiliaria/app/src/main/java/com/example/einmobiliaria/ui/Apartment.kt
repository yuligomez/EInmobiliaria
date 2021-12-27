package com.example.einmobiliaria.ui

import android.content.Intent
import android.graphics.Paint
import android.os.Bundle
import android.util.Log
import android.view.Menu
import android.view.MenuItem
import android.view.View
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.RecyclerView
import com.example.einmobiliaria.MainActivity
import com.example.einmobiliaria.R
import com.example.einmobiliaria.repository.RepositorySingleton
import com.example.einmobiliaria.adapters.AdapterBrokenElements
import com.example.einmobiliaria.adapters.AdapterElements
import com.example.einmobiliaria.models.out.ElementDetailInfoModel
import com.example.einmobiliaria.services.RestApiService
import com.google.android.material.chip.Chip

class Apartment : AppCompatActivity() {

    lateinit var  locationAparment : TextView
    lateinit var  nameApartment : TextView
    lateinit var  descriptionApartment : TextView
    lateinit var  chipElements : Chip
    lateinit var  chipBrokenElements : Chip
    lateinit var  addElementBtn : View
    lateinit var  addRentApartmentBtn : View
    lateinit var  recycler : RecyclerView
    lateinit var  bundle : Bundle
    private  var latitudDouble : Double  ? = null
    private  var longitudDouble : Double ?  = null
    private  var idAparment : Int = 0
    private  var userId : Int = 0
    private lateinit var adapterElements : AdapterElements
    private lateinit  var brokenElements : ArrayList<ElementBroken>
    private   var brokenElementsEmpty = ArrayList<ElementBroken> ()
    private lateinit var adapterBrokenElements :AdapterBrokenElements
    private  var elementsEmpty = ArrayList<ElementDetailInfoModel> ()
    private lateinit  var elementsNotBrokenResponse : ArrayList<ElementDetailInfoModel>




    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_apartment)
        initViews()

        bundle = intent.extras!!

        userId = bundle.getInt("userId")
        nameApartment.text = bundle.getString("aparment")
        idAparment = bundle.getInt("aparmentId")
        descriptionApartment.text = bundle.getString("description")
        latitudDouble = intent.getDoubleExtra("location_lat", 0.0);
        longitudDouble = intent.getDoubleExtra("location_long", 0.0);
        elementsNotBrokenResponse = ArrayList<ElementDetailInfoModel>()
        brokenElements=  ArrayList<ElementBroken> ()

        val apiService = RestApiService()
        apiService.getElementsByIdAparment(idAparment) { s, arrayList ->
            if(arrayList?.count()>0){

                Log.d("Exito", "Se logro agregar elements apartaments")
                Log.d("Exito", " tamaño de lista de elementis del backend para id apartamenti : $idAparment = ${arrayList.size}")

               elementsNotBrokenResponse = arrayList.filter  { s -> !s.IsBroken && s.UserId == userId } as ArrayList<ElementDetailInfoModel>
                val elementsBrokenResponse = arrayList.filter  { s -> s.IsBroken && s.UserId == userId } as ArrayList<ElementDetailInfoModel>

                Log.d("Exito", " tamaño de lista elementsNotBroken response filtrado ${elementsNotBrokenResponse.size}")
                Log.d("Exito", " tamaño de lista elementsBroken response  filtrao ${elementsBrokenResponse.size}")



                for(element in elementsBrokenResponse) {
                    val elementName = element.Name
                    val elementCant = element.Amount
                    val elementImageUrl = element.Photo.Name
                    val id = element.Id
                    val elementBroken = ElementBroken(id, elementName, elementCant, elementImageUrl)
                    brokenElements.add(elementBroken)
                }
                Log.d("Exito", " tamaño de elements broken${brokenElements.size}")

            }else {
                Log.d("Error", "Error al hacer get apartments")
            }
        }

        locationAparment.setOnClickListener { v ->

            val act = Intent (this, MapsActivityApartmentLocation::class.java)
            act.putExtra("latitud", latitudDouble)
            act.putExtra("longitud", longitudDouble)
            startActivity(act)

        }

        chipElements.setOnClickListener {


            if (elementsNotBrokenResponse?.count()>0) {
                adapterElements = AdapterElements(elementsNotBrokenResponse)
                adapterElements.notifyDataSetChanged()
                recycler.adapter = adapterElements

            }else {
                adapterElements = AdapterElements(elementsEmpty)
                adapterElements.notifyDataSetChanged()
                recycler.adapter = adapterElements
                Toast.makeText(this, "La lista de elementos está vacía ", Toast.LENGTH_LONG).show()
            }

        }
        chipBrokenElements.setOnClickListener {

            if (brokenElements?.count()>0){
                adapterBrokenElements =  AdapterBrokenElements(brokenElements)
                adapterBrokenElements.notifyDataSetChanged()
                recycler.adapter = adapterBrokenElements

            }
            else {
                adapterBrokenElements =  AdapterBrokenElements(brokenElementsEmpty)
                adapterBrokenElements.notifyDataSetChanged()
                recycler.adapter = adapterBrokenElements
                Toast.makeText(this, "La lista de elementos rotos está vacía ", Toast.LENGTH_LONG).show()
            }

        }

        addElementBtn.setOnClickListener { view ->

            intent = Intent(this, AddElement::class.java)
            bundle = Bundle()
            val aparmentName = nameApartment.text.toString()
            bundle.putString("aparment", aparmentName)
            bundle.putInt("aparmentId", idAparment)
            bundle.putInt("userId", userId)
            val aparmentDescription =  descriptionApartment.text.toString()
            bundle.putString("description", aparmentDescription)
            bundle.putDouble("location_lat", latitudDouble!!)
            bundle.putDouble("location_long", longitudDouble!!)
            intent.putExtras(bundle)
            startActivity(intent)

        }
        addRentApartmentBtn.setOnClickListener { view ->
            intent = Intent(this, AddRent::class.java)
            bundle = Bundle()
            bundle.putDouble("location_long", longitudDouble!!)
            bundle.putInt("aparmentId", idAparment)
            intent.putExtras(bundle)
            startActivity(intent)

        }

        // agrego botón para ir hacia atrás
        supportActionBar!!.setDisplayHomeAsUpEnabled(true)


    }

    private fun initViews (){

        chipElements= findViewById(R.id.chipElementAparment)
        chipBrokenElements = findViewById(R.id.chipBrokenElementsAparment)
        recycler = findViewById(R.id.RecyclerAparment)
        nameApartment = findViewById(R.id.nameApartment)
        descriptionApartment = findViewById(R.id.descriptionAparment)
        locationAparment= findViewById(R.id.ubicacionAparment)
        locationAparment.setPaintFlags(Paint.UNDERLINE_TEXT_FLAG);
        addElementBtn =findViewById(R.id.addElementAparment)
        addRentApartmentBtn = findViewById(R.id.addRentAparment)


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
                true
            }
            R.id.nav_profile -> {
                val intent = Intent(this, EditProfile::class.java)
                var bundle = Bundle()
                bundle.putString("role", RepositorySingleton.user?.Role.toString())
                intent.putExtras(bundle)
                startActivity(intent)
                true
            }
            R.id.nav_dashboard -> {

                if (RepositorySingleton.user?.Role.toString() == "ADMIN") {
                    val intent = Intent(this, DashboardAdmin::class.java)
                    startActivity(intent)
                    true

                } else {
                    val intent = Intent(this, DashboardChecker::class.java)
                    startActivity(intent)
                    true
                }

            }
            else -> return super.onOptionsItemSelected(item)
        }
    }


}