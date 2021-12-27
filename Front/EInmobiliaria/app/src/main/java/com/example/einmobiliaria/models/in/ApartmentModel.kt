package com.example.einmobiliaria.models.`in`

import com.google.gson.annotations.SerializedName
//import java.io.Serializable

data class ApartmentModel(
    @SerializedName("name")
    val Name: String,
    @SerializedName("description")
    val Description: String,
    @SerializedName("latitude")
    val Latitude: String,
    @SerializedName("longitude")
    val Longitude: String
)