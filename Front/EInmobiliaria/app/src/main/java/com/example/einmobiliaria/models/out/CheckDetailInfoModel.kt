package com.example.einmobiliaria.models.out

import com.example.einmobiliaria.models.StateEnum
import com.google.gson.annotations.SerializedName
import java.util.*

data class CheckDetailInfoModel(
    @SerializedName("id")
    val Id : Int,
    @SerializedName("userId")
    val UserId : Int,
    @SerializedName("user")
    val User : UserBasicInfoModel,
    @SerializedName("apartmentId")
    val ApartmentId : Int,
    @SerializedName("apartment")
    val Apartment : ApartmentBasicInfoModel,
    @SerializedName("checkDate")
    val CheckDate : String,
    @SerializedName("state")
    val State : String
)
