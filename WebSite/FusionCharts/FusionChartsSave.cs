using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Imaging;

public class FusionChartsSave : Page {
	//Decoded data from charts.
	String data;
	//Rows of color values.
	String[] rows;
	//Width and height of chart.
	int width;
	int height;
	//Default background color of the chart
	String bgcolor;
	Color bgColor;
	//Bitmap to store the chart.
	Bitmap chart;
	//Reference to graphics object
	Graphics gr;
	
    public void Page_Load(Object sender, EventArgs e) {		
		try{
			//Get the width and height from form
			width = Int32.Parse(Request["width"]);
			height = Int32.Parse(Request["height"]);		
		}catch(Exception ex){
			//If the width and height has not been given, we cannot create the image.
			Response.Write("Image width and height not provided.");
			Response.End();
		}
		//Background color, request and set default
		bgcolor = Request["bgcolor"];
		if (bgcolor=="" || bgcolor==null){
			bgcolor = "FFFFFF";
		}
		//Conver the bg color in ASP.NET format
		bgColor = ColorTranslator.FromHtml("#" + bgcolor);
		
		//Decompress the data
		data = Request["data"];
		
		//Parse data 
		rows = new String[height+1];
		rows = data.Split(';');
		
		//Build the image now
		buildBitmap();
		
		//Output it now
		Response.ContentType = "image/jpeg";		
		Response.AddHeader("Content-Disposition", "attachment; filename=\"FusionCharts.jpg\"");
		
		//Now, we need to encode the image using an encoder.
		//Find the encoder.
		ImageCodecInfo[] encoders;
		ImageCodecInfo img_encoder = null;
		encoders = ImageCodecInfo.GetImageEncoders();
		foreach (ImageCodecInfo codec in encoders){
			if (codec.MimeType == Response.ContentType){
				img_encoder = codec;
				break;
			}
		}
		//Set the quality as 100
		EncoderParameter jpeg_quality = new EncoderParameter(Encoder.Quality, 100L);
		EncoderParameters enc_params = new EncoderParameters(1);
		//Convey the parameter
		enc_params.Param[0] = jpeg_quality;
		
		//Save to output stream after applying proper encoders
		chart.Save(Response.OutputStream, img_encoder, enc_params);		
		
		//Dispose the bitmap
		gr.Dispose();
		chart.Dispose();						
    }
	/**
	 * Builds the bitmap representation of the chart from color values.
	*/
	private void buildBitmap(){
		chart = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
		gr = Graphics.FromImage(chart);
		gr.Clear(bgColor);		
		String c;
		int r;
		int ri = 0;
		for (int i=0; i<rows.Length; i++){
			//Split individual pixels.			
			String[] pixels = rows[i].Split(',');			
			//Set horizontal row index to 0
			ri = 0;
			for (int j=0; j<pixels.Length; j++){				
				//Now, if it's not empty, we process it				
				//Split the color and repeat factor
				String[] clrs = pixels[j].Split('_');	
				//Reference to color
				c = clrs[0];
				r = Int32.Parse(clrs[1]);
				//If color is not empty (i.e. not background pixel)
				if (c!=""){					
					if (c.Length<6){
						//If the hexadecimal code is less than 6 characters, pad with 0
						c = c.PadLeft(6,'0');
					}
					for (int k=1; k<=r; k++){						
						chart.SetPixel(ri, i, ColorTranslator.FromHtml("#" + c));
						//Increment horizontal row count
						ri++;						
					}
				}else{
					//Just increment horizontal index
					ri = ri + r;
				}
			}
		}
	}
}