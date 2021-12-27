package com.example.einmobiliaria.models.`in`
import com.google.gson.annotations.SerializedName

data class CheckPutModel(
    @SerializedName("userId")
    val UserId: Int,
    @SerializedName("state")
    val State: String
)
