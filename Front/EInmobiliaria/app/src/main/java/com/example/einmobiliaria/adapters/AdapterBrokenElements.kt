package com.example.einmobiliaria.adapters

import android.content.Intent
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.einmobiliaria.ui.EditElement
import com.example.einmobiliaria.ui.ElementBroken
import com.example.einmobiliaria.R
import com.squareup.picasso.Picasso

class AdapterBrokenElements(var list : List<ElementBroken>) :
    RecyclerView.Adapter<AdapterBrokenElements.ViewHolderBrokenElements>()
    {

    class ViewHolderBrokenElements(view : View) : RecyclerView.ViewHolder(view) {

        lateinit var bundle : Bundle
        lateinit var textHeader : String

        fun bindItem  (data : ElementBroken) {
            val name : TextView = itemView.findViewById(R.id.textNameBrokenElement)
            val cant : TextView = itemView.findViewById(R.id.textNumberBrokenElement)
            val imageView : ImageView = itemView.findViewById(R.id.imageBrokenElement)
            val id = data.id
            name.text = data.name
            cant.text = data.cant.toString()
            // convierto en imagen la url de la imagen
            Picasso.get().load("file://" + data.image).into(imageView)
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolderBrokenElements {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.item_element_broken_list, parent, false)
        return ViewHolderBrokenElements(view)
    }

    override fun onBindViewHolder(holder: ViewHolderBrokenElements, position: Int) {
        holder.bindItem(list[position])
    }

    override fun getItemCount(): Int = list.size
}