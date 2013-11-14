(function($){

	FX = window.FX || {} ;
	FX.k = "?X-API-KEY=XW787EpX54Lf2h72UqkRfOsSyOABdI";

	var map, center ;

	//initialize Navigation
	FX.slider = function()
	{
		//$('.section').ferroSlider();
	}

	FX.battery = function()
	{
		var val =  $('#batteryval').text();
		$('#intro, #firstbattery').addClass('full'+val);
	}

	//centering the intro
	FX.centering = function()
	{
		var W = $(window).width();
		var H = $(window).height();

		$('#center').css({
			'padding-top': ( H - $('#center').height() ) / 2 - 40 + 'px'
		});

		$('#container_weight').css({
			'top': ( H - $('#container_weight').height() ) / 2 - 70 ,
			'left': ( W - $('#container_weight').width() ) / 2 
		});

	}

	FX.resizeCanvas = function()
	{
		$("canvas").each(function(){
			$(this).attr('width' , $(this).parent().width() );
		});
	}

	// =============================== GRAPHS ================================

	/*
	* FITBIT Section 
	* Weight 
	*/

	FX.weight = function()
	{
		var theObjectif = 87,
			d = [],
			objectif = [], 
			l = [],
			lineGraphOpts = {
				scaleFontColor : "#0C2B38",
				scaleLineColor : "rgba(0,0,0,.2)",
				scaleShowGridLines : false,
				pointDotStrokeWidth : 0,
				datasetFill : false,
				pointDot : false,
				scaleOverride : true,	
				scaleSteps : 15,
				scaleStepWidth : 1,
				scaleStartValue : 85
			};

		$.getJSON("http://fxcardi.com/v0/weight" + this.k, null, function (r) {

			if(r){
				for(var i = r.length - 1 ; i > r.length - 90; i-- )
				{
					d.push( parseFloat( r[i].weight, 10 ) );

					if(i < r.length - 79)
					{
						objectif.push(theObjectif);
					}else{
						objectif.push(90);
					}
					
					l.push( (i == r.length - 1 || i == r.length - 89 ) ? r[i].datetime : "" );
				}

				var lineChartData = {
					labels : l.reverse() ,
					datasets : [
						{
							strokeColor : "#40BCD7",
							data : d.reverse()
						},
						{
							strokeColor : "#77B731",
							data : objectif
						}
					]
				};
				return new Chart( $('#graph_weight').get(0).getContext("2d") ).Line( lineChartData, lineGraphOpts );
			}
		});
	}

	/*
	* Steps
	*/
	FX.steps = function()
	{
		var d = [], l = [],
			stepsOpts = {
				scaleShowGridLines : false,
				scaleLabel : "<%=value%>"
			};

		$.getJSON('http://fxcardi.com/v0/steps' + this.k, null, function (r) {
			if(r)
			{
				for(var i = r.length - 1; i > r.length - 90; i--)
				{
					d.push( parseInt(r[i].steps,10));
					l.push( (i == r.length - 1 || i == r.length - 89 ) ? r[i].datetime : "" );
				}

				var barChartData = {
					labels : l.reverse(),
					datasets : [
						{
							fillColor : "rgba(119,183,49,.5)",
							strokeColor : "rgba(119,183,49,.5)",
							data : d.reverse()
						}
					]
				};
				return new Chart( $('#graph_steps').get(0).getContext("2d") ).Bar( barChartData, stepsOpts ) ;
			}
		});
	}

	FX.elevation = function( distance )
	{
		var el = [], l = [],
			lineGraphOpts = {
				scaleFontColor : "#0C2B38",
				scaleLineColor : "rgba(0,0,0,.2)",
				scaleShowGridLines : false,
				pointDotStrokeWidth : 0,
				//datasetFill : false,
				pointDot : false
			};

		$.getJSON('http://fxcardi.com/v0/elevation' + this.k, null, function (r) {
			if(r)
			{
				for(var i = r.length - 1; i > r.length - 90; i-- )
				{
					el.push( parseInt(r[i].elevation, 10) );
					l.push( ( i== r.length - 1 || i == r.length - 89 ) ? r[i].datetime : "" );
				}
				
				var distElevationData = {
				labels : l.reverse(),
					datasets : [
						{
							strokeColor : "rgba(75,120,129,.6)",
							fillColor : "rgba(75,120,129,.6)",
							data : el.reverse()
						}
					]
				};
				return new Chart( $('#graph_distelevation').get(0).getContext("2d") ).Line( distElevationData, lineGraphOpts ) ;
			}
		});
	}

	/*
	* SLEEP
 	*/
 	FX.sleep = function()
 	{
 		var d = [];
 		$.getJSON('http://fxcardi.com/v0/sleep' + this.k, null, function (r) {
 			if(r)
 			{
 				for(var i in r)
 				{
 					d.push( parseInt(r[i].minutesAsleep,10) / 60 );
 				}
 				
 				var sleepData = {

 				}
 			}
 		})
 	}

	/*
	* TOP 10 4sq checkins group by category 
	*/

	FX.checkins = function()
	{
		var d = [],
			treeColors = [ "rgba(236, 102, 59, 1)","rgba(66, 189, 238, 1)","rgba(66, 189, 238, .8)","rgba(66, 189, 238, .7)","rgba(66, 189, 238, .6)","rgba(66, 189, 238, .4)","rgba(118, 159, 205, 1)","rgba(118, 183, 49, 1)","rgba(233, 75 ,109, 1)","rgba(153, 132, 120, 1)"],
			treeMapOpts = {
				squareFontFamily : "'steel'",
				squareFontSize: 25,
				squareShowLabels: true
			};

		$.getJSON('http://fxcardi.com/v0/treemap' + this.k, null, function (r) {
			if(r)
			{
				for(var i in r)
				{
					d.push({
						value : parseInt( r[i].num, 10 ),
						color : treeColors[i],
						label : r[i].category,
						labelColor : "#FFF"
					});
				};
				return new Chart( $('#graph_checkins').get(0).getContext("2d") ).TreeMap( d , treeMapOpts );
			}
		});
	}

	/*
	* Personnal tracking Coffee 
	*/

	FX.coffee = function()
	{
		var d = [], l = [],
			radarOpts = {
				pointDotRadius : 2
			};
		$.getJSON('http://fxcardi.com/v0/coffee?' + this.k, null, function (r) {
			if(r)
			{
				for(var i = r.results.length - 1; i > r.results.length - 8; i--)
				{
					d.push( parseInt( r.results[i].coffees,10) );
					l.push( r.results[i].date );
				}

				var radarChartData = {
					labels : l,
					datasets : [
						{
							fillColor : "#998478",
							strokeColor : "#998478",
							pointColor : "#0C2B38",
							pointStrokeColor : "#0C2B38",
							data: d
						}
					]
				};

				return new Chart( $('#graph_coffee').get(0).getContext("2d") ).Radar( radarChartData, radarOpts );

			}
		});
	}

	FX.trips = function()
	{
	    $.getJSON('http://fxcardi.com/v0/trips?' + this.k, null, function (r) {
			if(r)
			{
				var colors = [];
				colors["Train"] = "#77B731",
				colors["Airport"] = colors["Airport Terminal"] = "#4B6581",
				colors["Boat"] = "#E94B6D",
				console.log(r);
				for(var i in r)
				{
					var startMarkerOpts = {
						radius : 3,
						color : "#fff",
						weight : 1,
						opacity : 1,
						fillColor : colors[ r[i].startCat ] ,
						fillOpacity : 1,
						clickable : false
					},
					endMarkerOpts = {
						radius : 3,
						color : "#fff",
						weight : 1,
						opacity : 1,
						fillColor : colors[ r[i].endCat ] ,
						fillOpacity : 1,
						clickable : false
					},
					polylineOpts = {
						color : colors[ r[i].startCat ],
						weight : 2,
						opacity : .7,
						fillColor : colors[ r[i].startCat ],
						fillOpacity : 1,
						clickable : false
					};

					var startlatlng = L.latLng( parseFloat(r[i].startLat, 10) , parseFloat(r[i].startLng, 10) );
					var endlatlng = L.latLng( parseFloat(r[i].endLat, 10) , parseFloat(r[i].endLng, 10) );

					var startmark = L.circleMarker( startlatlng, startMarkerOpts ).addTo(map);
					var endmark = L.circleMarker( endlatlng, endMarkerOpts ).addTo(map);

					var polyline = L.polyline([startlatlng, endlatlng], polylineOpts ).addTo(map);

				}
			}
		});
	}

	/* ================================== DOM Ready ======================================= */

	$(function(){
		FX.battery();
		FX.trips();
		FX.slider();
		FX.centering();
		FX.resizeCanvas();

		FX.weight();
		FX.coffee();
		FX.steps();
		FX.elevation();
		FX.checkins();

		$('#firstbattery').hover(function()
		{
			$('#lastupdate').show();
		}, function(){
			$('#lastupdate').hide();
		});

		$('#arrowdown').click(function(){
			$('html,body').animate({scrollTop:$("#weight").offset().top },'slow');
		});

		$(window).resize(function(){
			FX.centering();			
		});

		$('.arrow').click(function(e){
			e.preventDefault();
			$.fn.ferroSlider.slideTo( $(this).attr('data-go') );
			return false;
		});

	});

})(jQuery);