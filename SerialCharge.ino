const int R1 = 3;
const int R2 = 5;
const int R3 = 6;
const int G1 = 9;
const int G2 = 10;
const int G3 = 11;
const int led = 13;
const int AIA = 7;
const int AIB = 8;

void setup() {
    Serial.begin(9600); // set serial speed
    pinMode(led, OUTPUT);
    pinMode(AIA, OUTPUT);
    pinMode(AIB, OUTPUT);
    pinMode(R1, OUTPUT);
    pinMode(R2, OUTPUT);
    pinMode(R3, OUTPUT);
    pinMode(G1, OUTPUT);
    pinMode(G2, OUTPUT);
    pinMode(G3, OUTPUT);
    digitalWrite(led, LOW); //turn off LED
    digitalWrite(AIA, LOW);
    digitalWrite(AIB, LOW); //hold glass
}

void VoltageOn() {
    analogWrite(R1, 160);
    analogWrite(R2, 160);
    analogWrite(R3, 160);
    analogWrite(G1, 160);
    analogWrite(G2, 160);
    analogWrite(G3, 160);
}

void VoltageOff() {
    analogWrite(R1, 0);
    analogWrite(R2, 0);
    analogWrite(R3, 0);
    analogWrite(G1, 0);
    analogWrite(G2, 0);
    analogWrite(G3, 0);
}

void loop(){
    while (Serial.available() == 0); // do nothing if nothing sent
    int val = Serial.read() - '0'; // deduct ascii value of '0' to find numeric value of sent number
    
    switch (val) 
    {
      case 0:
       VoltageOff();
       digitalWrite(led, LOW); // turn off LED
       digitalWrite(AIA, LOW); // discharge glass
       digitalWrite(AIB, LOW);
      break;
      case 1:
       VoltageOn();
       digitalWrite(led, HIGH); // turn on LED
       digitalWrite(AIA, HIGH); // charge glass
       digitalWrite(AIB, LOW);
      break;
      case 2:
       VoltageOff();
       digitalWrite(led, LOW); // turn off LED
       digitalWrite(AIA, HIGH); // discharge glass
       digitalWrite(AIB, HIGH);
      break;
      case 3:
       VoltageOn();
       digitalWrite(led, HIGH); // turn off LED
       digitalWrite(AIA, LOW); // discharge glass
       digitalWrite(AIB, HIGH);
      break;
      case 4:
       VoltageOff();
       digitalWrite(led, LOW); // turn off LED
       digitalWrite(AIA, LOW); // discharge glass
       digitalWrite(AIB, LOW);
      break;
    }
    Serial.flush(); // clear serial port
}
