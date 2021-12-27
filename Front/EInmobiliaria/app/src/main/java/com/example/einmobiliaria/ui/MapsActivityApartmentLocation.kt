package com.example.einmobiliaria.ui

import android.Manifest
import android.content.pm.PackageManager
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.Toast
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import com.example.einmobiliaria.R
import com.example.einmobiliaria.ui.MapsActivity.Companion.REQUEST_CODE_LOCATION
import com.google.android.material.floatingactionbutton.FloatingActionButton
import com.google.android.gms.maps.CameraUpdateFactory
import com.google.android.gms.maps.GoogleMap
import com.google.android.gms.maps.OnMapReadyCallback
import com.google.android.gms.maps.SupportMapFragment
import com.google.android.gms.maps.model.LatLng
import com.google.android.gms.maps.model.MarkerOptions

class MapsActivityApartmentLocation : AppCompatActivity(), OnMapReadyCallback {

    private lateinit var mMap: GoogleMap
    private  var latitudDouble : Double  ? = null
    private  var longitudDouble : Double ?  = null
    private  val PERMISSION_ID = 42
   // private lateinit var  btnVolver : FloatingActionButton

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        setContentView(R.layout.activity_maps_apartment_location)
        // Obtain the SupportMapFragment
        val mapFragment = supportFragmentManager
            .findFragmentById(R.id.map) as SupportMapFragment
        mapFragment.getMapAsync(this)

       // btnVolver.findViewById<FloatingActionButton>(R.id.buttonVolverMapLocation)

        latitudDouble = intent.getDoubleExtra("latitud", 0.0);
        longitudDouble = intent.getDoubleExtra("longitud", 0.0);

        if (isPermissionsGranted() ) {
            Toast.makeText(this, "Generando mapa de ubicación", Toast.LENGTH_SHORT)
                .show()
        }else {
            requestLocationPermission()
        }



    }

    private fun isPermissionsGranted() = ContextCompat.checkSelfPermission(
        this,
        Manifest.permission.ACCESS_FINE_LOCATION
    ) == PackageManager.PERMISSION_GRANTED



    override fun onMapReady(googleMap: GoogleMap) {

        mMap = googleMap
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
        mMap.isMyLocationEnabled= true
        mMap.uiSettings.isZoomControlsEnabled= true
        mMap?.apply {
            val currentLocation = LatLng(latitudDouble!!, longitudDouble!!)
            // val currentLocation = LatLng(-34.903809, -56.191097)
            addMarker(
                MarkerOptions().position(currentLocation).title("Ubicación de apartamento")
            ).showInfoWindow()
            mMap.moveCamera(CameraUpdateFactory.newLatLngZoom(currentLocation, 13f))
        }

    }

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
                PERMISSION_ID
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
                mMap.isMyLocationEnabled = true
            }else{
                Toast.makeText(this, "Revisar la configuración para activar la localización ", Toast.LENGTH_SHORT).show()
            }
            else -> {}
        }
    }

  /*  fun onclick(view: View) {

        onBackPressed()
    }*/

}