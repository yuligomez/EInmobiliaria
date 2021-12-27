package com.example.einmobiliaria.models.`in`

import com.google.gson.annotations.SerializedName

data class SessionModel(
    @SerializedName("email")
    val Email: String,
    @SerializedName("password")
    val Password: String)
