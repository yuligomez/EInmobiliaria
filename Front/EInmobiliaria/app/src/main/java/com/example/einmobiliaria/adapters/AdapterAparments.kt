package com.example.einmobiliaria.adapters

import android.content.Intent
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Filter
import android.widget.Filterable
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.einmobiliaria.*
import com.example.einmobiliaria.models.out.ApartmentBasicInfoModel
import com.example.einmobiliaria.ui.Apartment
import kotlin.properties.Delegates

class AdapterAparments () :
    RecyclerView.Adapter<AdapterAparments.ViewHolderApartments>(), Filterable
    {
        lateinit var listAparments : List<ApartmentBasicInfoModel>
        lateinit  var listFilter : List<ApartmentBasicInfoModel>
        lateinit var bundle : Bundle
        var aparmentName : String ? = null
        var aparmentDescription : String ? = null
        var aparmentState : String ? = null
        var location_lat : Double ? = null
        var location_long : Double ? = null
        private var idAparment by Delegates.notNull<Int>()
        private var userId by Delegates.notNull<Int>()


        fun setData (list : List<ApartmentBasicInfoModel>, userId : Int){
            this.listAparments = list
            this.listFilter =list
            this.userId = userId
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
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): AdapterAparments.ViewHolderApartments {
        // inflo la view
        val view =
            LayoutInflater.from(parent.context)
                .inflate(R.layout.item_element_list, parent, false)
        return AdapterAparments.ViewHolderApartments(view)
    }


    override fun getItemCount() : Int = listAparments.size

    // comunica el adpatador con la clase ViewHolderElements

    override fun onBindViewHolder(holder: ViewHolderApartments, position: Int) {
        val element = listAparments[position]
        holder.bind(element.Name)

        holder.itemView.setOnClickListener { v ->

            val intent = Intent(v.context, Apartment::class.java)

            aparmentName= element.Name
            aparmentDescription = element.Description
            location_lat = element.Latitude.toDouble()
            location_long = element.Longitude.toDouble()
            aparmentState = element.State
            idAparment = element.Id
            bundle = Bundle()
            bundle.putString("aparment",aparmentName)
            bundle.putInt("aparmentId",idAparment)
            bundle.putInt("userId",userId)
            bundle.putString("description",aparmentDescription)
            bundle.putDouble("location_lat", location_lat!!)
            bundle.putDouble("location_long", location_long!!)
            bundle.putString("state",aparmentState)
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
                            if (item.Name.contains(searchChr) ){
                                itemList.add(item.Name)
                            }
                        }
                        filterResults.count = itemList.size
                        filterResults.values = itemList
                    }
                    return filterResults
                }

                override fun publishResults(constraint: CharSequence?, filterResults: FilterResults?) {
                    listAparments = filterResults!!.values as ArrayList<ApartmentBasicInfoModel>
                    notifyDataSetChanged()
                }


            }
        }

        fun update(modelList:ArrayList<ApartmentBasicInfoModel>){
            this.listAparments = modelList
            notifyDataSetChanged()
        }

    }
