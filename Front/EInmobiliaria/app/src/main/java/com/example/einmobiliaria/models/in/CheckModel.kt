package com.example.einmobiliaria.models.`in`

import com.google.gson.annotations.SerializedName

data class CheckModel(
    @SerializedName("rentalsId")
    val RentalsId: List<Integer> = emptyList(),
    @SerializedName("apartmentsId")
    val ApartmentsId: List<Integer> = emptyList(),
    @SerializedName("userId")
    val UserId: Int
)
