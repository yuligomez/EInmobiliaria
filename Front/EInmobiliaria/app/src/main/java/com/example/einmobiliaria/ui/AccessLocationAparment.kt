package com.example.einmobiliaria.ui

import androidx.appcompat.app.AppCompatActivity
import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import com.example.einmobiliaria.databinding.ActivityAccessLocationAparmentBinding


class AccessLocationAparment : AppCompatActivity() {


    private  var latitudDouble : Double  ? = null
    private  var longitudDouble : Double ?  = null

    // Iniciación tardía del viewBinding
    lateinit var binding : ActivityAccessLocationAparmentBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityAccessLocationAparmentBinding.inflate(layoutInflater)
        setContentView(binding.root)

        latitudDouble = intent.getDoubleExtra("location_lat", 0.0);
        longitudDouble = intent.getDoubleExtra("location_long", 0.0);


        binding.btnVerMapa.setOnClickListener(){
            Toast.makeText(this, " ubicación ${latitudDouble} ${longitudDouble}", Toast.LENGTH_LONG).show()
            val act = Intent (this, MapsActivityApartmentLocation::class.java)
            act.putExtra("latitud", latitudDouble)
            act.putExtra("longitud", longitudDouble)
            startActivity(act)
        }
    }

}



