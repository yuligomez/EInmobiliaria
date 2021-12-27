package com.example.einmobiliaria.ui

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import com.example.einmobiliaria.R
import com.example.einmobiliaria.repository.RepositorySingleton
import com.example.einmobiliaria.models.`in`.RentalModel
import com.example.einmobiliaria.models.out.ApartmentBasicInfoModel
import com.example.einmobiliaria.services.RestApiService
import java.time.format.DateTimeParseException
import kotlin.properties.Delegates

class AddRent : AppCompatActivity() {
    private lateinit var dateInitRent : EditText
    private lateinit var nameApartment : TextView
    private lateinit var datePicker: DatePickerFragment
    private lateinit var dateFinishRent: EditText
    private lateinit var dateInitFormat: String
    private lateinit var datefinishFormat: String
    private lateinit var buttonAdd: Button
    private lateinit var bundle: Bundle
    private var apartmentId = 0



    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_add_rent)
        initViews()
        bundle = intent.extras!!
        nameApartment.text = bundle.getString("aparment")
        apartmentId =  bundle.getInt("aparmentId")

        dateInitRent.setOnClickListener(){
            showDatePickerInitDialog()
        }
        dateFinishRent.setOnClickListener(){
            showDatePickerFinishDialog()
        }

        buttonAdd.setOnClickListener(){

            if (areValidDates()) {

                val newRentalAparment = RentalModel(apartmentId, dateInitFormat, datefinishFormat )
                val apiService = RestApiService()
                apiService.addRent(newRentalAparment){ s, rental ->
                    if(rental!=null){
                        Toast.makeText(this,"Alquier de Aparmento ${rental.Apartment.Name} registrado correctamente" ,
                                Toast.LENGTH_LONG).show()

                        RepositorySingleton.namesApartmentsToCheck?.add(rental.Apartment.Name)

                        intent = Intent(this, ApartmentsRegistered::class.java)
                        startActivity(intent)

                    } else {
                        Toast.makeText(this,s,Toast.LENGTH_LONG).show()
                    }
                }

                // actualizo la lista de rentas en la aplicación
                apiService.getRents{ s, rents ->
                    if(rents!=null){
                        Log.d("Exito","Se obtienen las rentas del   backend ")

                    }else{
                        Log.d("Error","Error al obtener las rentas del backend ")
                    }
                }
            }
            else {
                Toast.makeText(this,"Las fechas no pueden ser vacías" ,
                        Toast.LENGTH_SHORT).show()
            }

        }

    }

    private fun showDatePickerFinishDialog() {
        try {
        datePicker = DatePickerFragment {day, month, year -> onDateFinishtSelected(day, month, year)}
        datePicker.show(supportFragmentManager, "datePickerFinishRent")
        } catch (e : Exception){
            Toast.makeText(this,e.message,Toast.LENGTH_SHORT).show()
        }
    }

    private fun onDateFinishtSelected(day: Int, month: Int, year: Int) {
        dateFinishRent.setText("$year/$month/$day")
        datefinishFormat = "${year}/${month}/${day}"

    }

    private fun showDatePickerInitDialog() {
        try {
            datePicker = DatePickerFragment {day, month, year -> onDateInitSelected(day, month, year)}
            datePicker.show(supportFragmentManager, "datePickerInitRent")
        } catch (e : Exception){
            Toast.makeText(this,e.message,Toast.LENGTH_SHORT).show()
        }
    }

    private fun onDateInitSelected (day :  Int, month :Int , year : Int ){
        dateInitRent.setText("$year/$month/$day")
        dateInitFormat = "${year}/${month}/${day}"
    }

    private fun initViews() {
        dateInitRent = findViewById(R.id.etDateInitRent)
        dateFinishRent = findViewById(R.id.etDateFinishRent)
        nameApartment = findViewById(R.id.nameAparmentRent)
        buttonAdd = findViewById(R.id.buttonAdd)
    }

    private fun areValidDates () : Boolean {

        return !dateInitRent.text.isNullOrEmpty() &&
                !dateFinishRent.text.isNullOrEmpty()

    }

}