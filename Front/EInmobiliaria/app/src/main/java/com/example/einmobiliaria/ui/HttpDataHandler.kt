package com.example.einmobiliaria.ui

import java.io.BufferedReader
import java.io.IOException
import java.io.InputStreamReader
import java.net.HttpURLConnection
import java.net.MalformedURLException
import java.net.ProtocolException
import java.net.URL
import javax.net.ssl.HttpsURLConnection

class HttpDataHandler {
    fun getHttpData(requestURL: String?): String {
        val url: URL
        var response = ""
        try {
            url = URL(requestURL)
            val conn = url.openConnection() as HttpURLConnection
            conn.requestMethod = "GET"
            conn.readTimeout = 15000
            conn.connectTimeout = 15000
            conn.doInput = true
            conn.doOutput = true
            conn.setRequestProperty("Content-Type", "application/x-www-urlencoded")
            val responseCode = conn.responseCode
            if (responseCode == HttpsURLConnection.HTTP_OK) {
                var line: String
                val br = BufferedReader(InputStreamReader(conn.inputStream))
                while (br.readLine().also { line = it } != null) {
                    response += line
                }
            } else {
                response = ""
            }
        } catch (e: ProtocolException) {
            e.printStackTrace()
        } catch (e: MalformedURLException) {
            e.printStackTrace()
        } catch (e: IOException) {
            e.printStackTrace()
        }
        return response
    }

}