package com.example.einmobiliaria.ui

import android.Manifest
import android.app.AlertDialog
import android.content.Context
import android.content.Intent
import android.content.pm.PackageManager
import android.graphics.BitmapFactory
import android.net.Uri
import android.os.Bundle
import android.os.Environment
import android.provider.MediaStore
import android.util.Log
import android.view.View
import android.widget.*
import androidx.appcompat.app.AppCompatActivity
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import androidx.core.content.FileProvider
import androidx.loader.content.CursorLoader
import com.example.einmobiliaria.R
import com.example.einmobiliaria.repository.RepositorySingleton
import com.example.einmobiliaria.adapters.AdapterBrokenElements
import com.example.einmobiliaria.models.`in`.ElementModel
import com.example.einmobiliaria.services.RestApiService
import java.io.File
import java.io.IOException
import java.text.SimpleDateFormat
import java.util.*


class AddElement : AppCompatActivity() {

    private lateinit var bundle: Bundle
    private lateinit var isBroken: CheckBox
    private lateinit var buttonLoadImage: Button
    private lateinit var imageElementBroken: ImageView
    private lateinit var buttonAdd: Button
    private lateinit var elementName: EditText
    private lateinit var elementCant: EditText
    private  var apartmentId : Int = 0
    private var broken: Boolean = false
    private lateinit var mCurrentPhotoPath: String
    private var photoFile: File? = null
    private val COD_SELECCIONA = 10
    private val COD_FOTO = 20
    private lateinit var nameApartment : String
    private lateinit var descriptionApartment : String
    private  var idAparment : Int = 0
    private  var userId : Int = 0
    private  var latitudDouble : Double  ? = null
    private  var longitudDouble : Double ?  = null
    private var isChecker : Boolean = false


    override fun onCreate(savedInstanceState: Bundle?) {

        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_add_element)
        initViews()
        bundle = Bundle()
        bundle = intent.extras!!
        userId = bundle.getInt("userId")
        nameApartment = bundle.getString("aparment").toString()
        idAparment = bundle.getInt("aparmentId")
        descriptionApartment = bundle.getString("description").toString()
        latitudDouble = intent.getDoubleExtra("location_lat", 0.0);
        longitudDouble = intent.getDoubleExtra("location_long", 0.0);


        makeNoVisible( buttonLoadImage, imageElementBroken)

        isBroken.setOnClickListener{

            if(isBroken.isChecked){
                makeVisible( buttonLoadImage, imageElementBroken)
                broken = true
            }
            else {
                makeNoVisible( buttonLoadImage, imageElementBroken)
            }
        }
        val userId = RepositorySingleton.user?.Id

        buttonAdd.setOnClickListener(){

            if (areValidelement()){
                val name = elementName.text.toString()
                val valueCant: String = elementCant.text.toString()
                val img = imageElementBroken.imageAlpha
                val finalValueCant = valueCant.toInt()
                var elementModel : ElementModel
                val apiService = RestApiService()
                if (!broken){

                    elementModel = userId?.let { it1 ->
                        ElementModel(name,
                                finalValueCant, broken, "no tiene", 0, idAparment, it1)
                    }!!
                }
                else {

                    elementModel = userId?.let { it1 ->
                        ElementModel(name,
                                finalValueCant, broken, mCurrentPhotoPath, img, idAparment, it1)
                    }!!
                }

                apiService.addElement(elementModel) { s, element ->
                    if (element != null) {
                        RepositorySingleton.elementsAparments.add(element)

                        Toast.makeText(this@AddElement, "Se agregó el elemento ${element.Name} ", Toast.LENGTH_SHORT).show()
                    }
                }
                if (RepositorySingleton.user?.Role.toString() == "ADMIN"){
                    val intent = Intent(this, Apartment::class.java)
                    bundle = Bundle()
                    bundle.putString("aparment",nameApartment)
                    bundle.putInt("aparmentId",idAparment)
                    bundle.putInt("userId",userId)
                    bundle.putString("description",descriptionApartment)
                    bundle.putDouble("location_lat", latitudDouble!!)
                    bundle.putDouble("location_long", longitudDouble!!)
                    intent.putExtras(bundle)
                    startActivity(intent)
                }
                else {

                    val intent = Intent(this, Checking::class.java)
                    bundle = Bundle()
                    bundle.putString("aparment",nameApartment)
                    bundle.putInt("aparmentId",idAparment)
                    bundle.putInt("userId",userId)
                    bundle.putString("description",descriptionApartment)
                    bundle.putDouble("location_lat", latitudDouble!!)
                    bundle.putDouble("location_long", longitudDouble!!)
                    intent.putExtras(bundle)
                    startActivity(intent)

                }

            }
            else // no son válidos los elementos de la vista
            {

                    Toast.makeText(this@AddElement, "Los campos nombre y cantidad no pueden ser vacíos", Toast.LENGTH_LONG).show()
            }
        }

        // agrego botón para ir hacia atrás
        supportActionBar!!.setDisplayHomeAsUpEnabled(true)
    }


    private fun initViews() {
        isBroken = findViewById(R.id.checkBoxIsBroken)
        imageElementBroken = findViewById(R.id.imageElementBroken)
        buttonAdd = findViewById(R.id.buttonAdd)
        elementName = findViewById(R.id.textNameElement)
        elementCant=  findViewById(R.id.textQuantity)
        buttonLoadImage =  findViewById(R.id.buttonLoadImage)

    }

    private fun makeVisible(
            buttonLoadImage: Button,
            imageElementBroken: ImageView
    ) {
        imageElementBroken.visibility = View.VISIBLE
        buttonLoadImage.visibility = View.VISIBLE
    }

    private fun makeNoVisible(
            buttonLoadImage: Button,
            imageElementBroken: ImageView
    ) {
        imageElementBroken.visibility = View.GONE
        buttonLoadImage.visibility = View.GONE
    }

    fun onclick(view: View) {
        loadImage()
    }

    private fun loadImage() {

        val options = arrayOf<CharSequence>("Tomar Foto", "Cargar Imagen", "Cancelar")
        val alertOptions = AlertDialog.Builder(this@AddElement)
        alertOptions.setTitle("Seleccione una opción")
        alertOptions.setItems(options) { dialog, i ->
            if (options[i] == "Tomar Foto") {
                takePhoto()

            } else {
                if (options[i] == "Cargar Imagen") {

                    intent = Intent(Intent.ACTION_GET_CONTENT, android.provider.MediaStore.Images.Media.EXTERNAL_CONTENT_URI)
                    intent.type = "image/"
                    startActivityForResult(intent, COD_SELECCIONA)
                } else {
                    dialog.dismiss()
                }
            }
        }
        alertOptions.show()
    }

    private fun takePhoto() {

        if (ContextCompat.checkSelfPermission(
                        this,
                        Manifest.permission.CAMERA
                ) != PackageManager.PERMISSION_GRANTED
        ) {
            ActivityCompat.requestPermissions(
                    this,
                    arrayOf(Manifest.permission.CAMERA, Manifest.permission.WRITE_EXTERNAL_STORAGE),
                    0
            )
        } else {

            // creo e inicio intento de camara
            val takePictureIntent = Intent(MediaStore.ACTION_IMAGE_CAPTURE)
            if (takePictureIntent.resolveActivity(packageManager) != null) {
                // Create the File where the photo should go
                try {

                    photoFile = createImageFile()
                    displayMessage(baseContext, photoFile!!.getAbsolutePath())
                    Log.i("Ruta de almacenamiento", photoFile!!.getAbsolutePath())
                    // Continue only if the File was successfully created
                    if (photoFile != null) {


                        var photoURI = FileProvider.getUriForFile(
                                this,
                                "com.example.einmobiliaria.fileprovider",
                                photoFile!!
                        )
                        //recibo el resultado del intento
                        takePictureIntent.putExtra(MediaStore.EXTRA_OUTPUT, photoURI)
                        takePictureIntent.addFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION);
                        startActivityForResult(takePictureIntent, COD_FOTO)

                    }
                } catch (ex: Exception) {
                    // Error occurred while creating the File
                    displayMessage(baseContext, "Ha ocurrido in error de camára: " + ex.message.toString())
                }


            } else {
                displayMessage(baseContext, "Null")
            }
        }
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)
        if (resultCode == RESULT_OK) {

            when (requestCode) {
                COD_SELECCIONA -> {

                    val selectedImage: Uri? = data?.data
                    imageElementBroken.setImageURI(selectedImage)
                    val picturePath: String = getRealPathFromURI(this,
                            selectedImage)
                    mCurrentPhotoPath = picturePath

                }
                COD_FOTO -> {

                    val myBitmap = BitmapFactory.decodeFile(photoFile!!.getAbsolutePath())
                    imageElementBroken.setImageBitmap(myBitmap)
                }
            }
        }
    }


   open fun getRealPathFromURI(context: Context?, contentUri: Uri?): String {
       val proj = arrayOf(MediaStore.Images.Media.DATA)
       val loader = CursorLoader(context!!, contentUri!!, proj, null, null, null)
       val cursor = loader.loadInBackground()
       val column_index = cursor!!.getColumnIndexOrThrow(MediaStore.Images.Media.DATA)
       cursor!!.moveToFirst()
       val result = cursor!!.getString(column_index)
       cursor!!.close()
       return result
   }



    private fun displayMessage(context: Context, message: String) {
        Log.i("DEBUG", message)
        Toast.makeText(context, message, Toast.LENGTH_LONG).show()
    }


    @Throws(IOException::class)
    private fun createImageFile(): File {
        // Create an image file name
        val timeStamp = SimpleDateFormat("yyyyMMdd_HHmmss").format(Date())
        val imageFileName = "JPEG_" + timeStamp + "_"
        val storageDir = getExternalFilesDir(Environment.DIRECTORY_PICTURES)
      //  val storageDir = getOutputDirectory()

        val image = File.createTempFile(
                imageFileName, /* prefix */
                ".jpg", /* suffix */
                storageDir      /* directory */
        )

        // Save a file: path for use with ACTION_VIEW intents
        mCurrentPhotoPath = image.absolutePath
        return image
    }


    override fun onRequestPermissionsResult(
            requestCode: Int,
            permissions: Array<String>,
            grantResults: IntArray
    ) {
        if (requestCode == 0) {
            if (grantResults.isNotEmpty() && grantResults[0] == PackageManager.PERMISSION_GRANTED
                    && grantResults[1] == PackageManager.PERMISSION_GRANTED
            ) {
                takePhoto()

            }
        }
    }

    private fun areValidelement () : Boolean {

        return !elementName.text.isNullOrEmpty() &&
                !elementCant.text.isNullOrEmpty()

    }

}