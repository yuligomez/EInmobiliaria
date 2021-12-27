package com.example.einmobiliaria.models.out

import com.google.gson.annotations.SerializedName
import java.util.*

data class SessionBasicModel(
    @SerializedName("token")
    val Token: String,
)
