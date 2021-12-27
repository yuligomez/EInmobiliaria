package com.example.einmobiliaria.models.out

import com.google.gson.annotations.SerializedName

data class PhotoBasicInfoModel(
    @SerializedName("id")
    val Id : Integer,
    @SerializedName("name")
    val Name : String,
    @SerializedName("image")
    val Image : Int,
)
