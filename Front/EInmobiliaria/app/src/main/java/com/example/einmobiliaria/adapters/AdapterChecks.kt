package com.example.einmobiliaria.adapters

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Filter
import android.widget.Filterable
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.einmobiliaria.*
import com.example.einmobiliaria.models.`in`.CheckPutModel
import com.example.einmobiliaria.models.out.CheckDetailInfoModel
import com.example.einmobiliaria.repository.RepositorySingleton
import com.example.einmobiliaria.services.RestApiService
import com.example.einmobiliaria.ui.Checking
import kotlin.properties.Delegates

class AdapterChecks () :
        RecyclerView.Adapter<AdapterChecks.ViewHolderApartments>(), Filterable
{
    private lateinit var listAparments : List<CheckDetailInfoModel>
    private lateinit  var listFilter : List<CheckDetailInfoModel>
    private lateinit var bundle : Bundle
    private var aparmentName : String ? = null
    private var aparmentDescription : String ? = null
    private var aparmentState : String ? = null
    private var location_lat : Double ? = null
    private var location_long : Double ? = null
    private var isApartmentChecked : Boolean = false
    private lateinit var state : String
    private var idAparment by Delegates.notNull<Int>()


    fun setData (list : List<CheckDetailInfoModel>,isChecked : Boolean, stateToUpdate : String ){
        this.listAparments = list
        this.listFilter =list
        this.isApartmentChecked = isChecked
        this.state  = stateToUpdate
        notifyDataSetChanged()
    }

    class ViewHolderApartments (view : View) : RecyclerView.ViewHolder(view) {
        var element: TextView
        init {
            element = itemView.findViewById(R.id.idElement)
        }
        fun bind(e: String) {
            element.text = e

        }
    }

    //enlaza el adptador con el archivo item_element_list.xml
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): AdapterChecks.ViewHolderApartments {
        // inflo la view
        val view =
                LayoutInflater.from(parent.context)
                        .inflate(R.layout.item_element_list, parent, false)
        return AdapterChecks.ViewHolderApartments(view)
    }


    override fun getItemCount() : Int = listAparments.size

    // comunica el adpatador con la clase ViewHolderElements

    override fun onBindViewHolder(holder: ViewHolderApartments, position: Int) {
        val element = listAparments[position]

        holder.bind(element.Apartment.Name)

        holder.itemView.setOnClickListener { v ->

            val userId = RepositorySingleton.user?.Id
            val checkPutModel  = userId?.let { CheckPutModel (it,  this.state ) }
            val checkId = element.Id

            //ACTUALIZO ESTADO DEL CHECK
            val apiService = RestApiService()

            apiService.updateCheck(checkId, checkPutModel!!) { s, checkDetailInfoModel ->
                if (checkDetailInfoModel != null) {

                    Log.d("Exito", "Se logro hacer el update de check")
                } else {
                    Log.d("Error", "Error al hacer hacer el update de check")
                }
            }
            //actualizo lista de checks
            apiService.getAllChecks(){ s, arrayList ->
                if (arrayList!=null){
                    RepositorySingleton.checks.clear()
                    RepositorySingleton.checks.addAll(arrayList)
                }
            }

            val intent = Intent(v.context, Checking::class.java)
            aparmentName= element.Apartment.Name
            aparmentDescription = element.Apartment.Description
            location_lat = element.Apartment.Latitude.toDouble()
            location_long = element.Apartment.Longitude.toDouble()
            aparmentState = element.Apartment.State
            idAparment = element.Apartment.Id
            bundle = Bundle()
            bundle.putString("aparment",aparmentName)
            bundle.putInt("aparmentId",idAparment)
            bundle.putString("description",aparmentDescription)
            bundle.putDouble("location_lat", location_lat!!)
            bundle.putDouble("location_long", location_long!!)
            bundle.putBoolean("isApartmentChecked",isApartmentChecked)
            intent.putExtras(bundle)
            v.context.startActivity(intent)

        }
    }

    override fun getFilter(): Filter {
        return object : Filter() {
            override fun performFiltering(charseSequence: CharSequence?): FilterResults {
                val filterResults = FilterResults()
                if (charseSequence == null || (charseSequence!!.length < 0)) {
                    filterResults.count = listFilter.size
                    filterResults.values = listFilter
                } else {
                    var searchChr = charseSequence.toString().toLowerCase()
                    val itemList = ArrayList<String>()
                    for (item in listFilter){
                        if (item.Apartment.Name.contains(searchChr) ){
                            itemList.add(item.Apartment.Name)
                        }
                    }
                    filterResults.count = itemList.size
                    filterResults.values = itemList
                }
                return filterResults
            }

            override fun publishResults(constraint: CharSequence?, filterResults: FilterResults?) {
                listAparments = filterResults!!.values as ArrayList<CheckDetailInfoModel>
                notifyDataSetChanged()
            }


        }
    }



}
