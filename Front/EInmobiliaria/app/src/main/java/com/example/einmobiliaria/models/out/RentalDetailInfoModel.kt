package com.example.einmobiliaria.models.out

import com.google.gson.annotations.SerializedName
import java.util.*

data class RentalDetailInfoModel(
    @SerializedName("id")
    val Id: Integer,
    @SerializedName("apartmentId")
    val ApartmentId: Integer,
    @SerializedName("apartment")
    val Apartment: ApartmentBasicInfoModel,
    @SerializedName("startDate")
    val StartDate: String,
    @SerializedName("endingDate")
    val EndingDate: String
)
