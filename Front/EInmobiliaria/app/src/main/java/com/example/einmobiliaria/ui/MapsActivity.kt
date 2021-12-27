package com.example.einmobiliaria.ui

import android.Manifest
import android.annotation.SuppressLint
import android.content.Context
import android.content.Intent
import android.content.pm.PackageManager
import android.location.Location
import android.location.LocationManager
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.Looper
import android.provider.Settings
import android.widget.Toast
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import com.example.einmobiliaria.CreateApartment
import com.example.einmobiliaria.R
import com.google.android.gms.location.*
import com.google.android.gms.location.LocationServices
import com.google.android.gms.maps.CameraUpdateFactory
import com.google.android.gms.maps.GoogleMap
import com.google.android.gms.maps.OnMapReadyCallback
import com.google.android.gms.maps.SupportMapFragment
import com.google.android.gms.maps.model.LatLng
import com.google.android.gms.maps.model.MarkerOptions
import com.google.android.material.floatingactionbutton.FloatingActionButton

class MapsActivity : AppCompatActivity(), OnMapReadyCallback , GoogleMap.OnMyLocationButtonClickListener, GoogleMap.OnMyLocationClickListener{

    private lateinit var map: GoogleMap
    private lateinit var btnVolver: FloatingActionButton
    private lateinit var fusedLocationClient: FusedLocationProviderClient
    private lateinit var latitude: String
    private lateinit var longitude: String
    private  var latitudDouble : Double  ? = null
    private  var longitudDouble : Double ?  = null
    private var name : String  ? = null
    private var description : String  ? = null

    val PERMISSION_ID = 42

    companion object {
        const val REQUEST_CODE_LOCATION = 0
        private val REQUIRED_PERMISSION_GPS = arrayOf( android.Manifest.permission.ACCESS_COARSE_LOCATION,
                android.Manifest.permission.ACCESS_FINE_LOCATION)
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_maps)
        createFragment()

        val bundle = intent.extras!!
        name  =bundle.getString ("nameApto", "")
        description  =bundle.getString("descriptionApto", "")



        btnVolver = findViewById(R.id.floatingActionButtonVolver)

        if(allPermissionGarantedGPS()) {
            fusedLocationClient = LocationServices.getFusedLocationProviderClient(this)
            leerubicacionactual()
        }else // sino hay permisos los solicito al usuario{
            ActivityCompat.requestPermissions(this,
                    arrayOf(android.Manifest.permission.ACCESS_COARSE_LOCATION,
                            android.Manifest.permission.ACCESS_FINE_LOCATION), PERMISSION_ID)


        btnVolver.setOnClickListener { view ->

            Toast.makeText(this, "Te encuentras ubicado en ${latitude}, ${longitude}", Toast.LENGTH_SHORT).show()
            intent = Intent(this, CreateApartment::class.java)
            var bundle = Bundle()
            bundle.putString("nameApto",name)
            bundle.putString("descriptionApto",description)
            intent.putExtras(bundle)

            startActivity(intent)
        }
    }

    private fun leerubicacionactual(){
        if (checkPermissions()){
            if (isLocationEnabled()){

                // si tengo los permisos,solicito la última ubicación
                if (ActivityCompat.checkSelfPermission(this,
                                android.Manifest.permission.ACCESS_COARSE_LOCATION) ==
                        PackageManager.PERMISSION_GRANTED &&
                        ActivityCompat.checkSelfPermission(this, android.Manifest.permission.ACCESS_COARSE_LOCATION)
                        == PackageManager.PERMISSION_GRANTED) {

                    fusedLocationClient.lastLocation.addOnCompleteListener(this){ task ->
                        var location: Location? = task.result
                        if (location == null){
                            requestNewLocationData()
                        } else {
                            latitude = "LATITUD = " + location.latitude.toString()
                            longitude = "LONGITUD = " + location.longitude.toString()
                            latitudDouble= location.latitude
                            longitudDouble = location.longitude
                        }
                    }
                }
            } else {
                Toast.makeText(this, "Activar ubicación", Toast.LENGTH_SHORT).show()
                val intent = Intent(Settings.ACTION_LOCATION_SOURCE_SETTINGS)
                startActivity(intent)
                this.finish()
            }
        } else {
            ActivityCompat.requestPermissions(this,
                    arrayOf(android.Manifest.permission.ACCESS_COARSE_LOCATION,
                            android.Manifest.permission.ACCESS_FINE_LOCATION), PERMISSION_ID)
        }
    }

    @SuppressLint("MissingPermission")
    private fun requestNewLocationData(){
        var mLocationRequest = LocationRequest()
        mLocationRequest.priority = LocationRequest.PRIORITY_HIGH_ACCURACY
        mLocationRequest.interval = 0
        mLocationRequest.fastestInterval = 0
        mLocationRequest.numUpdates = 1
        fusedLocationClient = LocationServices.getFusedLocationProviderClient(this)
        fusedLocationClient.requestLocationUpdates(mLocationRequest, mLocationCallBack, Looper.myLooper())
    }


    private val mLocationCallBack = object : LocationCallback(){
        override fun onLocationResult(locationResult: LocationResult) {
            var mLastLocation : Location = locationResult.lastLocation
            latitude = "LATITUD = " + mLastLocation.latitude.toString()
            longitude = "LONGITUD = "+ mLastLocation.longitude.toString()
            latitudDouble = mLastLocation.latitude
            longitudDouble = mLastLocation.longitude
        }
    }

    // creo el mapa
    private fun createFragment() {
        // Obtain the SupportMapFragment and get notified when the map is ready to be used.
        val mapFragment = supportFragmentManager
                .findFragmentById(R.id.map) as SupportMapFragment
        mapFragment.getMapAsync(this)
    }

    override fun onMapReady(googleMap: GoogleMap) {
        // se crea mapa
        map = googleMap
        // se agrega marker
        createMarker()
        map.setOnMyLocationButtonClickListener(this)
        map.setOnMyLocationClickListener(this)
        // se activa la localización en tiempo real
        enableMyLocation()

    }

    private fun createMarker() {
        val coordinates = LatLng(-34.903809, -56.191097)

        val marker = MarkerOptions().position(coordinates).title("Marker Ort")
        map.addMarker(marker)
        map.animateCamera(CameraUpdateFactory.newLatLngZoom(coordinates, 18f), 4000, null)
    }

    private fun isPermissionsGranted() = ContextCompat.checkSelfPermission(
            this,
            Manifest.permission.ACCESS_FINE_LOCATION
    ) == PackageManager.PERMISSION_GRANTED


    private fun checkPermissions(): Boolean {
        if (ActivityCompat.checkSelfPermission(this, android.Manifest.permission.ACCESS_COARSE_LOCATION) == PackageManager.PERMISSION_GRANTED &&
                ActivityCompat.checkSelfPermission(this, android.Manifest.permission.ACCESS_FINE_LOCATION) == PackageManager.PERMISSION_GRANTED
        ) {
            return true
        }
        return false
    }
    private fun allPermissionGarantedGPS()= MapsActivity.REQUIRED_PERMISSION_GPS.all {
        ContextCompat.checkSelfPermission(baseContext, it) == PackageManager.PERMISSION_GRANTED
    }

    private fun isLocationEnabled(): Boolean {
        var locationManager: LocationManager = getSystemService(Context.LOCATION_SERVICE) as LocationManager
        return locationManager.isProviderEnabled(LocationManager.GPS_PROVIDER) || locationManager.isProviderEnabled(
                LocationManager.NETWORK_PROVIDER
        )
    }


    // intenta activar la localización del dispositivo
    private fun enableMyLocation() {
        if (!::map.isInitialized) return  // si el mapa no está inicializado salgo

        // si los permisos ya están aceptados
        if (isPermissionsGranted()) {
            if (ActivityCompat.checkSelfPermission(
                            this,
                            Manifest.permission.ACCESS_FINE_LOCATION
                    ) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(
                            this,
                            Manifest.permission.ACCESS_COARSE_LOCATION
                    ) != PackageManager.PERMISSION_GRANTED
            ) {

                return
            }
            map.isMyLocationEnabled = true // activo la localización en tiempo real

        } else {  // sino tengo permisos , los pido
            requestLocationPermission()
        }
    }

    // le pido al usuario que acepete los permisos
    private fun requestLocationPermission() {
        if (ActivityCompat.shouldShowRequestPermissionRationale(
                        this,
                        Manifest.permission.ACCESS_FINE_LOCATION
                )
        ) {
            // ya le había pedido permiso y no los aceptó
            Toast.makeText(this, "Revisar los permisos desde la configuración", Toast.LENGTH_SHORT)
                    .show()
        } else {
            // la primera vez que le solicito los permisos al usuario
            ActivityCompat.requestPermissions(
                    this,
                    arrayOf(Manifest.permission.ACCESS_FINE_LOCATION),
                    REQUEST_CODE_LOCATION
            )
        }
    }

    // capturo la decisión del usuario de aceptar o no los  permisos
    override fun onRequestPermissionsResult(
            requestCode: Int,
            permissions: Array<out String>,
            grantResults: IntArray
    ) {
        when(requestCode){
            REQUEST_CODE_LOCATION -> if(grantResults.isNotEmpty() && grantResults[0]==PackageManager.PERMISSION_GRANTED){
                if (ActivityCompat.checkSelfPermission(
                                this,
                                Manifest.permission.ACCESS_FINE_LOCATION
                        ) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(
                                this,
                                Manifest.permission.ACCESS_COARSE_LOCATION
                        ) != PackageManager.PERMISSION_GRANTED
                ) {
                    return
                }
                map.isMyLocationEnabled = true // activo la localización en tiempo real

            }else{
                Toast.makeText(this, "Revisar la configuración para activar la localización ", Toast.LENGTH_SHORT).show()
            }
            else -> {}
        }
    }

    // si desactivo los permisos desde los ajustes
    override fun onResumeFragments() {
        super.onResumeFragments()
        if (!::map.isInitialized) return
        if(!isPermissionsGranted()){
            if (ActivityCompat.checkSelfPermission(
                            this,
                            Manifest.permission.ACCESS_FINE_LOCATION
                    ) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(
                            this,
                            Manifest.permission.ACCESS_COARSE_LOCATION
                    ) != PackageManager.PERMISSION_GRANTED
            ) {

                return
            }
            map.isMyLocationEnabled = false
            Toast.makeText(this, "Para activar la localización ve a ajustes y acepta los permisos", Toast.LENGTH_SHORT).show()
        }
    }

    override fun onMyLocationButtonClick(): Boolean {
        Toast.makeText(this, "Accediendo a ubicación en tiempo real....", Toast.LENGTH_SHORT).show()
        // con return false me lleva a mi localización en tiempo real si apreto el botón !

        return false
    }



    // se llama cada vez que el usuario selecciona el punto de localización en el mapa , luego de darle al botón de localización
    override fun onMyLocationClick(p0: Location) {
        val act = Intent(this, CreateApartment::class.java)
        act.putExtra("nameApto", name)
        act.putExtra("descriptionApto", description)
        act.putExtra("latitud", p0.latitude)
        act.putExtra("longitud", p0.longitude)
        startActivity(act)
        Toast.makeText(this, "Te encuentras ubicado en ${p0.latitude}, ${p0.longitude}", Toast.LENGTH_SHORT).show()
    }
}
