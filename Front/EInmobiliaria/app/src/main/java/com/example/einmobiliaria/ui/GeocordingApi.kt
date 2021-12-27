package com.example.einmobiliaria.ui

import android.app.ProgressDialog
import android.net.wifi.WifiConfiguration.AuthAlgorithm.strings
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import com.example.einmobiliaria.R
import org.json.JSONArray
import org.json.JSONException
import org.json.JSONObject
import android.os.AsyncTask as AsyncTask

class GeocordingApi : AppCompatActivity() {

    private lateinit var showCoord : Button;
    private lateinit var editAdress : EditText;
    private lateinit var txtCoord : TextView;

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_geocording_api)
        initViews()
        showCoord.setOnClickListener(){
            GetCoordinates().execute(editAdress.text.toString().replace(" ", "+"))
        }
    }

    private fun initViews() {
        showCoord = findViewById(R.id.btnShowCoordinates)
        editAdress = findViewById(R.id.edtAddress)
        txtCoord = findViewById(R.id.txtCoordinates)

    }

    inner class GetCoordinates : AsyncTask<String?, Void?, String?>() {
        var dialog = ProgressDialog(this@GeocordingApi)
        override fun onPreExecute() {
            super.onPreExecute()
            dialog.setMessage("Porfavor espere...")
            dialog.setCanceledOnTouchOutside(false)
            dialog.show()
        }

        override fun onPostExecute(s: String?) {
            try {
                val jsonObject = JSONObject(s)
                val lat =
                    (jsonObject["results"] as JSONArray).getJSONObject(0).getJSONObject("geometry")
                        .getJSONObject("location")["lat"].toString()
                val lng =
                    (jsonObject["results"] as JSONArray).getJSONObject(0).getJSONObject("geometry")
                        .getJSONObject("location")["lng"].toString()
                txtCoord.setText(String.format("Cooridantes : %s/ %s", lat, lng))

                if (dialog.isShowing()){
                    dialog.dismiss()
                }

            } catch (e: JSONException) {
                e.printStackTrace()
            }
        }


        override fun doInBackground(vararg params: String?): String? {
            var response :String
            try {
                val address = strings[0]
                val http = HttpDataHandler()
                val url = String.format(
                    "https://maps.googleapis.com/maps/api/geocode/json?address=%s",
                    address
                )
                response = http.getHttpData(url)
                return response
            } catch (e: Exception) {

            }
            return null
        }
    }

}





