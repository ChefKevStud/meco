import processing.video.*;

Movie video;

PImage backImg;
PImage backReplace;

float errorMargin = 45;

void setup() {
  size (640, 360);
  noStroke();
  video = new Movie(this, "whale.mp4");
  video.loop();
  
  backImg = createImage(video.width, video.height, RGB);
  backReplace = loadImage("galaxy.jpg");
}

void draw() {
  
  if (video.available() == true) {
    loadPixels();
    video.read();
    video.loadPixels();
    
    for (int x = 0; x < video.width; x ++ ) {
      for (int y = 0; y < video.height; y ++ ) {
        int loc = x + y*video.width;
        color fgColor = video.pixels[loc];
  
        float r1 = red(fgColor);
        float g1 = green(fgColor);
        float b1 = blue(fgColor);
        
        float diff1 = g1 - r1;
        float diff2 = g1 - b1;

        if (diff1 < errorMargin && diff2 < errorMargin) {
          pixels[loc] = fgColor;
        } else {
          pixels[loc] = backReplace.pixels[loc];
        }
      }
    }
    updatePixels();
  }
}
