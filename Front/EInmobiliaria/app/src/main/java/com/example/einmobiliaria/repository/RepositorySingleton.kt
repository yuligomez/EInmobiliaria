package com.example.einmobiliaria.repository

import com.example.einmobiliaria.models.out.*
import com.example.einmobiliaria.ui.ElementBroken


object RepositorySingleton {

    var listElementsChecking: ArrayList<String>? = null

    var listElementsAparment: ArrayList<String>? = null

    var aparmentsRegistered: ArrayList<ApartmentBasicInfoModel>

    var namesApartmentsRegistered : ArrayList<String>? = null

    var aparments: ArrayList<ApartmentBasicInfoModel>

    var aparmentsToCheck: ArrayList<CheckDetailInfoModel>

    var aparmentsToNotify: ArrayList<RentalDetailInfoModel>

    var namesApartmentsToCheck : ArrayList<String>? = null

    var aparmentsInCheck: ArrayList<CheckDetailInfoModel>

    var namesApartmentsInCheck : ArrayList<String>? = null

    var aparmentsChecked: ArrayList<CheckDetailInfoModel>

    var namesApartmentsChecked : ArrayList<String>? = null

    var elementsAparments: ArrayList<ElementDetailInfoModel>

     var checks : ArrayList<CheckDetailInfoModel>

    var token: String = ""
    var user: UserBasicInfoModel? = null

    init {

        aparments = ArrayList <ApartmentBasicInfoModel> ()
        aparmentsRegistered = ArrayList <ApartmentBasicInfoModel> ()
        elementsAparments = ArrayList <ElementDetailInfoModel> ()
        aparmentsToCheck = ArrayList <CheckDetailInfoModel> ()
        aparmentsInCheck = ArrayList <CheckDetailInfoModel> ()
        aparmentsChecked = ArrayList <CheckDetailInfoModel> ()
        aparmentsToNotify = ArrayList <RentalDetailInfoModel> ()
        namesApartmentsToCheck = ArrayList <String> ()
        namesApartmentsRegistered = ArrayList <String> ()
        namesApartmentsInCheck = ArrayList <String> ()
        namesApartmentsChecked = ArrayList <String> ()
        checks = ArrayList <CheckDetailInfoModel> ()

    }
}


