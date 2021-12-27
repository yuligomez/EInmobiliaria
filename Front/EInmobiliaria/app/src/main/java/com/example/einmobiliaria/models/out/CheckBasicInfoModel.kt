package com.example.einmobiliaria.models.out

import com.example.einmobiliaria.models.StateEnum
import com.google.gson.annotations.SerializedName
import java.util.*

data class CheckBasicInfoModel(
    @SerializedName("id")
    val Id : Integer,
    @SerializedName("userId")
    val UserId : Integer,
    @SerializedName("apartmentId")
    val ApartmentId : Integer,
    @SerializedName("checkDate")
    val CheckDate : String,
    @SerializedName("state")
    val State : String)
