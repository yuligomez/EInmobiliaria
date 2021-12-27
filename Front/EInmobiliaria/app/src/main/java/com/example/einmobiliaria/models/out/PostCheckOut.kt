package com.example.einmobiliaria.models.out

import com.google.gson.annotations.SerializedName

data class PostCheckOut(
    @SerializedName("rents")
    val rents : List<RentalBasicInfoModel>,
    @SerializedName("checks")
    val checks : List<CheckBasicInfoModel>
)
