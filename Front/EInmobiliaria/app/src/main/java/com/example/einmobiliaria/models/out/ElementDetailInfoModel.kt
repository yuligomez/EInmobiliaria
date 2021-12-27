package com.example.einmobiliaria.models.out

import com.google.gson.annotations.SerializedName

data class ElementDetailInfoModel(
    @SerializedName("id")
    val Id: Int,
    @SerializedName("name")
    val Name: String,
    @SerializedName("amount")
    val Amount: Int,
    @SerializedName("isBroken")
    val IsBroken: Boolean,
    @SerializedName("photoId")
    val PhotoId: Int ?,
    @SerializedName("photo")
    val Photo: PhotoBasicInfoModel,
    @SerializedName("apartmentId")
    val ApartmentId: Int,
    @SerializedName("apartment")
    val Apartment: ApartmentBasicInfoModel,
    @SerializedName("userId")
    val UserId: Int,
    @SerializedName("user")
    val User: UserBasicInfoModel

)


