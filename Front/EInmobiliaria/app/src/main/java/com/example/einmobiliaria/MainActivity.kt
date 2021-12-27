package com.example.einmobiliaria

import android.content.Intent
import android.os.Bundle
import android.os.Handler
import android.os.Looper
import android.util.Log
import android.view.WindowManager
import android.view.animation.Animation
import android.view.animation.AnimationUtils
import android.widget.*
import androidx.appcompat.app.AppCompatActivity
import com.example.einmobiliaria.repository.RepositorySingleton.aparmentsChecked
import com.example.einmobiliaria.repository.RepositorySingleton.aparmentsInCheck
import com.example.einmobiliaria.repository.RepositorySingleton.aparmentsRegistered
import com.example.einmobiliaria.repository.RepositorySingleton.aparmentsToCheck
import com.example.einmobiliaria.repository.RepositorySingleton.namesApartmentsToCheck
import com.example.einmobiliaria.models.out.ApartmentBasicInfoModel
import com.example.einmobiliaria.models.out.CheckBasicInfoModel
import com.example.einmobiliaria.models.out.CheckDetailInfoModel
import com.example.einmobiliaria.services.RestApiService
import com.example.einmobiliaria.ui.DashboardAdmin
import com.example.einmobiliaria.ui.LogIn
import org.w3c.dom.Text


class MainActivity : AppCompatActivity() {

    private lateinit var nameApp : TextView
    private lateinit var imgLogo : ImageView

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        window.setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN)

        //AREGO ANIMACIONES
        val animation1 : Animation = AnimationUtils.loadAnimation(this, R.anim.scroll_up)
        val animation2 : Animation = AnimationUtils.loadAnimation(this, R.anim.scroll_down)

        nameApp = findViewById(R.id.nameApp)
        imgLogo = findViewById(R.id.logoApp)

        nameApp.animation = animation2
        imgLogo.animation = animation1

        Handler(Looper.getMainLooper()).postDelayed(object : Runnable {
            override fun run() {
                intent = Intent(this@MainActivity, LogIn::class.java)
                startActivity(intent)
                finish()
            }
        },3000)

        val apiService = RestApiService()

        apiService.getAllApartments { s, arrayList ->

            if(arrayList?.count()>0){
                Log.d("Exito","Se logró cargar get all apartments desde backend ")
            }else {
                Log.d("Error","Error al hacer get apartments")
            }
        }

        apiService.getAllChecks { s, arrayList ->

           if(arrayList?.count()>0){

                Log.d("Exito","Se logró cargar la lista de checks desde el backend ")

            }else {
                Log.d("Error","Error al hacer get apartments")
            }
        }

        apiService.getRents{ s, rents ->
            if(rents!=null){
                Log.d("Exito","Se obtienen las rentas del   backend ")

            }else{
                Log.d("Error","Error al obtener las rentas del backend ")
            }
        }
    }
}
