import processing.video.*;

Movie video;

PImage backImg;
PImage backReplace;

float errorMargin = 10;

void setup() {
  size (640, 360);
  noStroke();
  video = new Movie(this, "whale.mp4");
  video.loop();
  
  backImg = createImage(video.width, video.height, RGB);
  backReplace = loadImage("galaxy.jpg");
}

//void captureEvent(Capture video) {
//  video.read();
//}

void draw() {
  errorMargin = map(mouseX, 0, width, 5, 50);
  
  if (video.available() == true) {
    loadPixels();
    video.read();
    video.loadPixels();
    backImg.loadPixels();
    
    for (int x = 0; x < video.width; x ++ ) {
      for (int y = 0; y < video.height; y ++ ) {
        int loc = x + y*video.width; // Step 1, what is the 1D pixel location
        color fgColor = video.pixels[loc]; // Step 2, what is the foreground color
  
        // Step 3, what is the background color
        color bgColor = backImg.pixels[loc];
  
        // Step 4, compare the foreground and background color
        float r1 = red(fgColor);
        float g1 = green(fgColor);
        float b1 = blue(fgColor);
        float r2 = red(bgColor);
        float g2 = green(bgColor);
        float b2 = blue(bgColor);
        float diff = dist(r1, g1, b1, r2, g2, b2);
  
        // Step 5, Is the foreground color different from the background color
        if (diff > errorMargin) {
          // If so, display the foreground color
          pixels[loc] = fgColor;
        } else {
          // If not, display the beach scene
          pixels[loc] = backReplace.pixels[loc];
        }
      }
    }
    updatePixels();
  }
}
