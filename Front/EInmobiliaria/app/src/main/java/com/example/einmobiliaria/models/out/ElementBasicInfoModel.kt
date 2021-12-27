package com.example.einmobiliaria.models.out

import com.google.gson.annotations.SerializedName

data class ElementBasicInfoModel(
        @SerializedName("id")
        val Id: Int,
        @SerializedName("name")
        val Name : String,
        @SerializedName("amount")
        val Amonut: Int,
        @SerializedName("isBroken")
        val IsBroken: Boolean,
        @SerializedName("photoId")
        val PhotoId: Int,
        @SerializedName("photo")
        val Photo: PhotoBasicInfoModel
)
