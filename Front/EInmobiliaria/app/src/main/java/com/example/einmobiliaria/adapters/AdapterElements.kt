package com.example.einmobiliaria.adapters

import android.content.Intent
import android.os.Bundle
import androidx.recyclerview.widget.RecyclerView
import com.example.einmobiliaria.adapters.AdapterElements.ViewHolderElements
import android.view.ViewGroup
import android.view.LayoutInflater
import android.view.View
import com.example.einmobiliaria.R
import android.widget.TextView
import com.example.einmobiliaria.ui.EditElement
import com.example.einmobiliaria.models.out.ElementDetailInfoModel

class AdapterElements(var listElements: List<ElementDetailInfoModel>?) :
        RecyclerView.Adapter<ViewHolderElements>() {

    lateinit var bundle : Bundle



    //enlaza el adptador con el archivo item_element_list.xml
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolderElements {
        // inflo la view
        val view =
                LayoutInflater.from(parent.context)
                        .inflate(R.layout.item_element_list, parent, false)
        return ViewHolderElements(view)
    }

    // comunica el adpatador con la clase ViewHolderElements
    override fun onBindViewHolder(holder: ViewHolderElements, position: Int) {
        val element = listElements?.get(position)
        holder.bind(element!!.Name)

    }

    // retorna el tamaño de la lista de elementos
    override fun getItemCount(): Int = listElements!!.size


    class ViewHolderElements(itemView: View) : RecyclerView.ViewHolder(itemView) {
        var element: TextView
        init {
            element = itemView.findViewById(R.id.idElement)
        }
        fun bind(e: String) {
            element.text = e

        }

    }
}