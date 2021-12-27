package com.example.einmobiliaria.ui

import android.content.Intent
import android.graphics.Paint
import android.os.Bundle
import android.util.Log
import android.view.Menu
import android.view.MenuItem
import android.view.View
import android.widget.*
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.RecyclerView
import com.example.einmobiliaria.MainActivity
import com.example.einmobiliaria.R
import com.example.einmobiliaria.repository.RepositorySingleton
import com.example.einmobiliaria.adapters.AdapterBrokenElements
import com.example.einmobiliaria.adapters.AdapterElements
import com.example.einmobiliaria.models.`in`.CheckPutModel
import com.example.einmobiliaria.models.out.ApartmentBasicInfoModel
import com.example.einmobiliaria.models.out.ElementDetailInfoModel
import com.example.einmobiliaria.services.RestApiService
import com.github.clans.fab.FloatingActionMenu
import com.google.android.material.chip.Chip

class Checking : AppCompatActivity()  {

    private lateinit var  chipElements : Chip
    private lateinit var  chipBrokenElements : Chip
    private lateinit var  recycler : RecyclerView
    private lateinit var  addElementBtn : View
    private lateinit var  doneCheckingBtn : View
    lateinit var  locationAparment : TextView
    lateinit var  nameApartment : TextView
    lateinit var  descriptionApartment : TextView
    private  var  idAparment :  Int = 0
    private lateinit var bundle : Bundle
    private  var latitudDouble : Double  ? = null
    private  var longitudDouble : Double ?  = null
    private lateinit var  brokenElements : ArrayList<ElementBroken>
    private lateinit var floatingMenu : FloatingActionMenu
    private  var isAparmentChecked : Boolean = false
    private lateinit var adapter : AdapterElements
    private lateinit var brokenElementsEmpty : ArrayList<ElementBroken>
    private lateinit var adapterBrokenElements : AdapterBrokenElements
    private lateinit var elementsEmpty : ArrayList<ElementDetailInfoModel>
    private lateinit var  elementsNotBrokenResponse : ArrayList<ElementDetailInfoModel>
    private lateinit var  elementsBrokenResponse : ArrayList<ElementDetailInfoModel>

    private lateinit var apiService : RestApiService
    private  var userId : Int? =  RepositorySingleton.user?.Id


    override fun onCreate(savedInstanceState: Bundle?) {

        super.onCreate(savedInstanceState)

        setContentView(R.layout.activity_checking)

        initViews()

        bundle = intent.extras!!

        // agrego botón para ir hacia atrás
        supportActionBar!!.setDisplayHomeAsUpEnabled(true)

        }

    private fun getCheckIdByIdAparment(idAparment: Int): Int {
        val checks = RepositorySingleton.checks
        var i = 0
        var found = false
        var checkId = 0
        while(!found && i < checks.size){
            if (checks[i].ApartmentId == idAparment){
                checkId = checks[i].Id
                found = true
            }
            i++
        }

        return checkId
    }


    private fun initViews() {

        chipElements= findViewById(R.id.chipElement)
        chipBrokenElements = findViewById(R.id.chipBrokenElements)
        recycler = findViewById(R.id.RecyclerChecking)
        addElementBtn = findViewById(R.id.floatingButtonAddElement)
        doneCheckingBtn = findViewById(R.id.floatingButtonDoneChecking)
        nameApartment = findViewById(R.id.nameApartment)
        descriptionApartment = findViewById(R.id.descriptionAparment)
        locationAparment= findViewById(R.id.ubicacionAparment)
        locationAparment.setPaintFlags(Paint.UNDERLINE_TEXT_FLAG);
        floatingMenu = findViewById(R.id.floatingActionMenuChecking)
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
                bundle.putString("role", RepositorySingleton.user?.Role.toString())
                intent.putExtras(bundle)
                startActivity(intent)
                true
            }
            R.id.nav_dashboard -> {

                if (RepositorySingleton.user?.Role.toString() ==  "ADMIN") {
                    val intent = Intent(this, DashboardAdmin::class.java)
                    startActivity(intent)
                    true

                }else {
                    val intent = Intent(this, DashboardChecker::class.java)
                    startActivity(intent)
                    true
                }

            }
            else -> return super.onOptionsItemSelected(item)
        }
    }

    override fun onResume() {
        super.onResume()
        idAparment = bundle.getInt("aparmentId")
        nameApartment.text = bundle.getString("aparment", "")
        descriptionApartment.text = bundle.getString("description", "")
        latitudDouble = intent.getDoubleExtra("location_lat", 0.0)
        longitudDouble = intent.getDoubleExtra("location_long", 0.0)
        isAparmentChecked = intent.getBooleanExtra("isApartmentChecked", false)


        if (isAparmentChecked) {
            floatingMenu.visibility = View.GONE
        }

        brokenElements = ArrayList<ElementBroken>()
        elementsNotBrokenResponse = ArrayList<ElementDetailInfoModel>()

        apiService = RestApiService()
        apiService.getElementsByIdAparment(idAparment) { s, arrayList ->
            if (arrayList?.count() > 0) {

                if (RepositorySingleton.user?.Role.toString() == "CHECKER") {

                    elementsNotBrokenResponse = arrayList.filter { s -> !s.IsBroken && s.UserId == userId } as ArrayList<ElementDetailInfoModel>
                    elementsBrokenResponse = arrayList.filter { s -> s.IsBroken && s.UserId == userId } as ArrayList<ElementDetailInfoModel>

                    Log.d("Exito", " tamaño de lista elementsNotBroken response filtrado ${elementsNotBrokenResponse.size}")
                    Log.d("Exito", " tamaño de lista elementsBroken response  filtrao ${elementsBrokenResponse.size}")

                    for (element in elementsBrokenResponse) {
                        val elementName = element.Name
                        val elementCant = element.Amount
                        val elementImageUrl = element.Photo.Name
                        val id = element.Id
                        val elementBroken = ElementBroken(id, elementName, elementCant, elementImageUrl)
                        brokenElements.add(elementBroken)
                    }
                    Log.d("Exito", " tamaño de elements broken${brokenElements.size}")

                }
                // apto DONE , cargo elementos del apartmaento pero con el userId del chequeador , para ver los elementos del apartamento chequeado y no mis elementos registrados
                else {


                    elementsNotBrokenResponse = arrayList.filter { s -> !s.IsBroken && s.User.Role.toString() ==  "CHECKER"} as ArrayList<ElementDetailInfoModel>
                    elementsBrokenResponse = arrayList.filter { s -> s.IsBroken &&  s.User.Role.toString() ==  "CHECKER" } as ArrayList<ElementDetailInfoModel>

                    Log.d("Exito", " tamaño de lista elementsNotBroken response filtrado ${elementsNotBrokenResponse.size}")
                    Log.d("Exito", " tamaño de lista elementsBroken response  filtrao ${elementsBrokenResponse.size}")

                    for (element in elementsBrokenResponse) {
                        val elementName = element.Name
                        val elementCant = element.Amount
                        val elementImageUrl = element.Photo.Name
                        val id = element.Id
                        val elementBroken = ElementBroken(id, elementName, elementCant, elementImageUrl)
                        brokenElements.add(elementBroken)
                    }
                    Log.d("Exito", " tamaño de elements broken${brokenElements.size}")

                }

            } else {
                Log.d("Error", "Error al hacer get apartments")
            }

        }

        chipElements.setOnClickListener {

            if (elementsNotBrokenResponse?.count() > 0) {

                adapter = AdapterElements(elementsNotBrokenResponse)
                recycler.adapter = adapter

            } else {
                elementsEmpty = ArrayList<ElementDetailInfoModel>()
                adapter = AdapterElements(elementsEmpty)
                recycler.adapter = adapter
                Toast.makeText(this, "La lista de elementos está vacía ", Toast.LENGTH_LONG).show()
            }

        }
        chipBrokenElements.setOnClickListener {

            if (brokenElements?.count() > 0) {

                adapterBrokenElements = AdapterBrokenElements(brokenElements)
                recycler.adapter = adapterBrokenElements
            } else {

                brokenElementsEmpty = ArrayList<ElementBroken>()
                adapterBrokenElements = AdapterBrokenElements(brokenElementsEmpty)
                recycler.adapter = adapterBrokenElements
                Toast.makeText(this, "La lista de elementos está vacía ", Toast.LENGTH_LONG).show()
            }

        }

        locationAparment.setOnClickListener { v ->

            val act = Intent (this, MapsActivityApartmentLocation::class.java)
            act.putExtra("latitud", latitudDouble)
            act.putExtra("longitud", longitudDouble)
            startActivity(act)

        }
        

        doneCheckingBtn.setOnClickListener { view ->

            val checkModel = userId?.let { CheckPutModel(it, "DONE") }
            val checkId = getCheckIdByIdAparment(idAparment)

            if (checkModel != null) {
                apiService.updateCheck(checkId, checkModel) {
                    s, checkDetailInfoModel ->

                    if(checkDetailInfoModel!= null){

                        Log.d("Exito","Se logro hacer el update de check a DONE luego de finalizar chequeo")
                    }else {
                        Log.d("Error","Error al hacer hacer el update de check a DONE luego de finalizar chequeo")
                    }
                }
            }
            // actualizo lista de checks con el nuevo estado actualizado para el check anterior
            apiService.getAllChecks { s, arrayList ->

                if(arrayList?.count()>0){

                    Log.d("Exito","Se logró cargar la lista de checks desde el backend ")

                }else {
                    Log.d("Error","Error al hacer get apartments")
                }
            }

            intent = Intent(this, DashboardChecker::class.java)
            Toast.makeText(this, "Aparmento ${nameApartment.text} chequeado correctamente",
                    Toast.LENGTH_LONG).show()
            startActivity(intent)
        }

        addElementBtn.setOnClickListener { view ->

            intent = Intent(this, AddElement::class.java)
            bundle = Bundle()
            val aparmentName = nameApartment.text.toString()
            bundle.putString("aparment", aparmentName)
            bundle.putInt("aparmentId", idAparment)
            RepositorySingleton.user?.let { bundle.putInt("userId", it.Id) }
            val aparmentDescription = descriptionApartment.text.toString()
            bundle.putString("description", aparmentDescription)
            bundle.putDouble("location_lat", latitudDouble!!)
            bundle.putDouble("location_long", longitudDouble!!)

            intent.putExtras(bundle)
            startActivity(intent)
        }

    }


}