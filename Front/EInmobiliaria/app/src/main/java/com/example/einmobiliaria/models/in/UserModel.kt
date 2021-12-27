package com.example.einmobiliaria.models.`in`

import com.google.gson.annotations.SerializedName

data class UserModel(
        @SerializedName("name")
        val Name: String,
        @SerializedName("email")
        val Email: String,
        @SerializedName("password")
        var Password: String,
        @SerializedName("role")
        val Role: String)