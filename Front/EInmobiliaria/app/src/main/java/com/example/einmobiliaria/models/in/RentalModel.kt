package com.example.einmobiliaria.models.`in`

import com.google.gson.annotations.SerializedName

data class RentalModel(
        @SerializedName("apartmentId")
        val ApartmentId: Int,
        @SerializedName("startDate")
        val StartDate: String,
        @SerializedName("endingDate")
        val EndingDate: String)
