#include <ArduinoJson.h>
#include <HTTPClient.h>
#include <ArduinoJson.h>
#include <WiFi.h>
#include <DFRobot_DHT11.h>
#include <HTTPCLIENT.h>
DFRobot_DHT11 DHT;
#define DHT11_In 19
#define DHT11_Out 18
#define Role1 2
#define Role2 15
//backend Url Endpoint
String url = "http://www.machine.somee.com/api/";
//machine power state
bool State = false;


//CLient User Setting
const String clientUserName = "Deneme123";
const String clientName = "Deneme123";
const String clientPassword = "Deneme123";

//wifi connect setting
const char* ssid = "TurkTelekom_ZT43RX_2.4GHz";
const char* password = "udscsf4ENKYs";

void setup(){
  //pin setup
  pinMode(Role1,OUTPUT);
  pinMode(Role2,OUTPUT);
  digitalWrite(Role1,LOW);
  digitalWrite(Role2,LOW);

  //sytem procecss
  Serial.begin(115200);
  delay(5000);
  Serial.println("Machine Starting");
  Serial.println("--------------------");
  
  //wifi connect 
  WiFi.begin(ssid, password);
  Serial.println("\Connecting To Wifi");

  while(WiFi.status() != WL_CONNECTED){
        Serial.print(".");
        delay(100);
    }
    
    Serial.println("\nConnected to the WiFi network");
    Serial.print("Local Client IP: ");
    Serial.println(WiFi.localIP());
    Serial.println("//////////////////////////////////////////");
    delay(5000);
    HTTPClient http;
    http.begin(url+"Client");
    http.addHeader("Content-Type", "application/json");
    int httpResponseCode = http.POST("{\"clientUserName\":\""+clientUserName+"\",\"clientPasswordEncrypt\":\""+clientPassword+"\",\"name\":\""+clientName+"\"}");
    if (httpResponseCode>0) {
    Serial.print("HTTP Response code: ");
    Serial.println(httpResponseCode);
    String payload = http.getString();
    Serial.println(payload);
    State = true;
    
    
  }
  else {
    Serial.print("Error code: ");
    Serial.println(httpResponseCode);
  }
  
}

void loop(){
  //
    if(State){
        HTTPClient http;
        http.begin(url+"Client/GetClient");
        http.addHeader("Content-Type", "application/json");
        int httpResponseCode = http.POST("{\"name\":\""+clientUserName+"\",\"password\":\""+clientPassword+"\"}");
         if (httpResponseCode>0) {
           StaticJsonDocument<512> doc;
           Serial.print("HTTP Response code: ");
           Serial.println(httpResponseCode);
           String payload = http.getString();
            DeserializationError error = deserializeJson(doc, payload);
            if (error) {
              Serial.print(F("deserializeJson() failed: "));
              Serial.println(error.f_str());
              return;
            }else{
                  bool mState = doc["state"];
                  double temperatureLimit = doc["temperatureLimit"];
          
                  Serial.println("MachineState"); 
                  Serial.println(mState); 
                  Serial.println(temperatureLimit);
          
                  if(mState){
                      //iç sıcaklık değerleri
                      DHT.read(DHT11_In);
                      Serial.print("İc Sıcaklık:");
                      Serial.print(DHT.temperature);
                      Serial.print("Iceri Nem Oranı:");
                      Serial.println(DHT.humidity);
                      
                      double insideTemperature = DHT.temperature; 
                      double insideHumudity = DHT.humidity;

                      //dış sıcaklık değerlerini okuma
                      DHT.read(DHT11_Out);
                      Serial.print("Dıs Sıcaklık:");
                      Serial.println(DHT.temperature);
                      Serial.println("Dısarı Nem Oranı:");
                      Serial.println(DHT.humidity);
                      
                      
                      double outSideTemperature = DHT.temperature;
                      double outHumudity = DHT.humidity;
                      
                      //pompa rolesi açılıyor
                      Serial.println("Pompa Aktif");
                      digitalWrite(Role2,HIGH);

                      String machineMessage= "";
                      //bu kısımda veriler alınaktadıur
                      if(insideTemperature < temperatureLimit){
                          machineMessage = "Sıcaklık Dengeleniyor";
                          Serial.println("Sıcaklık Dengeleniyor");
                          digitalWrite(Role1,HIGH);                          
                      }else{
                          machineMessage = "Sıcaklık Dengelendi";
                          Serial.println("Sıcaklık Dengelendi");
                          digitalWrite(Role1,LOW);
                        }
                  HTTPClient http;
                  http.begin(url+"Client/MachineUpdate");
                  http.addHeader("Content-Type", "application/json");
                  
                  DynamicJsonDocument doc(512);

                  doc["userName"] = clientUserName;
                  doc["password"]   = clientPassword;
                  doc["outTemperature"] = outSideTemperature;
                  doc["insideTemperature"] = insideTemperature;
                  doc["outHumudity"] = outHumudity;
                  doc["insideHumudity"] = insideHumudity;
                  doc["machineMessage"] = machineMessage;
                 String json = "";
                 serializeJson(doc, json);                   
                  int httpResponseCode = http.POST(json);            
                   if (httpResponseCode == 200) {
                        
                       Serial.println(httpResponseCode);                      
                       Serial.println("Makine Sorunsuz");
                    }else{
                       digitalWrite(Role1,LOW);
                       digitalWrite(Role1,LOW);
                       State = false;
                       Serial.println("Serverde bir hata ile karsılasıldı makine kapatılıyor");                     
                    }
                  
                  }else{
                    Serial.println("Machine Close");
                    digitalWrite(Role1,LOW);
                    digitalWrite(Role1,LOW);
                    }
                  
            }
        
  }
  else {
    Serial.print("Error code: ");
    State = false;
    digitalWrite(Role1,LOW);
    digitalWrite(Role1,LOW);
    Serial.println(httpResponseCode);
  }
    }else{
      Serial.print("Server Error");
      digitalWrite(Role1,LOW);
      digitalWrite(Role1,LOW);
    }
  delay(5000);
  
}
