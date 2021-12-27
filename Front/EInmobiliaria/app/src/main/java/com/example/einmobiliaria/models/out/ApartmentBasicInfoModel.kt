package com.example.einmobiliaria.models.out

import com.google.gson.annotations.SerializedName

data class ApartmentBasicInfoModel(
        @SerializedName("id")
        val Id: Int,
        @SerializedName("name")
        val Name: String,
        @SerializedName("description")
        val Description: String,
        @SerializedName("latitude")
        val Latitude: String,
        @SerializedName("longitude")
        val Longitude: String,
        @SerializedName("state")
        val State: String,)