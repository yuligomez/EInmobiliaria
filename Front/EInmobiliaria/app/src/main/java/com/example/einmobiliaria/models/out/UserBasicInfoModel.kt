package com.example.einmobiliaria.models.out

import com.example.einmobiliaria.models.RoleEnum
import com.google.gson.annotations.SerializedName

data class UserBasicInfoModel(
    @SerializedName("id")
    val Id: Int,
    @SerializedName("name")
    val Name: String,
    @SerializedName("email")
    val Email: String,
    @SerializedName("role")
    val Role: RoleEnum
)
