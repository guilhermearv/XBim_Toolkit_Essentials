var check = Viewer.check();
if (check.noErrors) {
	var viewer = new Viewer('viewer');
	viewer.on('loaded', function () {
		//Hide any loaders you have used to keep your user excited
		//while their precious models are being processed and loaded
		//and start the animation.
		viewer.start();
		viewer.show(ViewType.DEFAULT);
	});

	viewer.on('error', function (arg) {
		var container = document.getElementById('errors');
		if (container) {
			//preppend error report
			container.innerHTML = "<pre style='color:red;'>" + arg.message + "</pre> <br />" + container.innerHTML;
		}
	});

	viewer.on('fps', function (fps) {
		var span = document.getElementById('fps');
		if (span) {
			span.innerHTML = fps;
		}
	});

	viewer.on('pick', function (args) {
		if (args == null || args.id == null) {
			return;
		}

		//you can use ID for funny things like hiding or
		//recolouring which will be covered in one of the next tutorials
		var id = args.id;
		var modelId = args.model;
		var coords = `[${Array.from(args.xyz).map(c => c.toFixed(2))}]`;

		document.getElementById('productId').innerHTML = id;
		document.getElementById('modelId').innerHTML = modelId;
		document.getElementById('coordsId').innerHTML = coords;
	});

	viewer.on('dblclick', function (args) {
		if (args == null || args.id == null) {
			return;
		}
		viewer.zoomTo(args.id);
	});

	viewer.load("http://localhost:56127/download/SampleHouse.wexbim");
}
else {
	var msg = document.getElementById("msg");
	msg.innerHTML = '';
	for (var i in check.errors) {
		var error = check.errors[i];
		msg.innerHTML += "<div style='color: red;'>" + error + "</div>";
	}
}

function handleContextLost() {
	event.preventDefault();
	cancelAnimationFrame(requestId)
	init();
}