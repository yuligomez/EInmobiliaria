package com.example.einmobiliaria.models.`in`

import com.google.gson.annotations.SerializedName

data class ElementModel(

    @SerializedName("name")
    val Name: String,
    @SerializedName("amount")
    val Amount: Int,
    @SerializedName("isBroken")
    val IsBroken: Boolean,
    @SerializedName("photoName")
    val PhotoName: String,
    @SerializedName("image")
    val Image: Int,
    @SerializedName("apartmentId")
    val ApartmentId: Int,
    @SerializedName("userId")
    val UserId: Int,
)


