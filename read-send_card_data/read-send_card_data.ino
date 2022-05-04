
#include <SPI.h>
#include <MFRC522.h>
#include <ArduinoSTL.h>
#define RST_PIN         9           // Configurable, see typical pin layout above
#define SS_PIN          10          // Configurable, see typical pin layout above

MFRC522 mfrc522(SS_PIN, RST_PIN);   // Create MFRC522 instance

//*****************************************************************************************//
void setup() {
  Serial.begin(9600);                                           // Initialize serial communications with the PC
  SPI.begin();                                                  // Init SPI bus
  mfrc522.PCD_Init();                                              // Init MFRC522 card
  //shows in serial that it is ready to read
}

//*****************************************************************************************//
void loop() {

  // Prepare key - all keys are set to FFFFFFFFFFFFh at chip delivery from the factory.
  MFRC522::MIFARE_Key key;
  for (byte i = 0; i < 6; i++) key.keyByte[i] = 0xFF;

  //some variables we need
  byte block;
  byte len;
  MFRC522::StatusCode status;

  //-------------------------------------------

  // Reset the loop if no new card present on the sensor/reader. This saves the entire process when idle.
  mfrc522.PCD_StopCrypto1();
  if ( ! mfrc522.PICC_IsNewCardPresent()) {
    return;
  }

  // Select one of the cards
  if ( ! mfrc522.PICC_ReadCardSerial()) {
    return;
  }

  byte buffer2[20];
  block = 1;
  status = mfrc522.PCD_Authenticate(MFRC522::PICC_CMD_MF_AUTH_KEY_A, 1, &key, &(mfrc522.uid)); //line 834
  if (status != MFRC522::STATUS_OK) {
    Serial.print(F("Authentication failed: "));
    Serial.println(mfrc522.GetStatusCodeName(status));
    delay(1000); //change value if you want to read cards faster
    mfrc522.PCD_StopCrypto1();
    mfrc522.PICC_HaltA();
    return;
  }

  len = sizeof(buffer2);
  status = mfrc522.MIFARE_Read(block, buffer2, &len);
  if (status != MFRC522::STATUS_OK) {
    Serial.print(F("Reading failed: "));
    Serial.println(mfrc522.GetStatusCodeName(status));
    delay(1000); //change value if you want to read cards faster
    mfrc522.PCD_StopCrypto1();
    mfrc522.PICC_HaltA();
    return;
  }

  //PRINT Card_Id
  std::string card_id = "";
  for (uint8_t i = 0; i < 8; i++) {
    //Serial.write(buffer2[i]);
    card_id += buffer2[i];
    //Serial.println(card_id[i]);
  }
  Serial.println(card_id.c_str());
  Serial.flush();
  delay(1500);
  
}
//*****************************************************************************************//
