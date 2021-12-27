package com.example.einmobiliaria.adapters


import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Filter
import android.widget.Filterable
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.einmobiliaria.ui.DataAparment
import com.example.einmobiliaria.R
import com.example.einmobiliaria.models.out.ApartmentBasicInfoModel

class AdapterUnCheckAparments (var listApartments: List<String>, var clickListener: OnAparmentUncheckClickListener) :
        RecyclerView.Adapter<ViewHolderUnCheckAparments>() ,  Filterable
{

    lateinit  var listFilter : List<String>
    lateinit var listAparments : List<String>
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolderUnCheckAparments {

        val view = LayoutInflater.from(parent.context).inflate(
                R.layout.item_element_list,
                parent, false
        )
        return ViewHolderUnCheckAparments(view)
    }

    override fun onBindViewHolder(holder: ViewHolderUnCheckAparments, position: Int) {
        holder.asignElement(listApartments[position], clickListener)
    }

    override fun getItemCount(): Int = listApartments.size

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
                        if (item.contains(searchChr) ){
                            itemList.add(item)
                        }
                    }
                    filterResults.count = itemList.size
                    filterResults.values = itemList
                }
                return filterResults
            }

            override fun publishResults(constraint: CharSequence?, filterResults: FilterResults?) {
                listAparments = filterResults!!.values as ArrayList<String>
                notifyDataSetChanged()
            }
        }
    }

}

class ViewHolderUnCheckAparments  (itemView: View) : RecyclerView.ViewHolder(itemView){
    var element: TextView

    fun asignElement(incomingElement: String, action : OnAparmentUncheckClickListener){

        element.text = incomingElement
        itemView.setOnClickListener {
            action.onAparmentClick(incomingElement, adapterPosition)
        }
    }
    init {
        element = itemView.findViewById(R.id.idElement)
    }

}

interface OnAparmentUncheckClickListener{

    fun onAparmentClick(item : String, position: Int){
    }
}
