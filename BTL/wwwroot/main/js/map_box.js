mapboxgl.accessToken =
    "pk.eyJ1IjoiYW5obnYxMjMiLCJhIjoiY2xmaTB4NnFwMWJ5aTN5b2h2aDd0M3ljayJ9.cfrj_8029UjpGWCEkwRHcA";
const map = new mapboxgl.Map({
    container: "map_box", // container ID
    style: "mapbox://styles/mapbox/streets-v12", // style URL
    center: [105.80372, 21.02716], // starting position [lng, lat]
    zoom: 17, // starting zoom
});
var marker = new mapboxgl.Marker({
    color: "red", //Màu của Marker là đỏ
    draggable: true,
    anchor: "bottom", //Nhãn Hà Nội nằm dưới Marker
})
    .setLngLat([105.80372, 21.02716]) //Thiết lập Marker tại hà Nội
    .addTo(map);

var popup = new mapboxgl.Popup({
    closeButton: true,
    closeOnClick: false,
    anchor: 'left',
}).setLngLat([105.80372, 21.02716])
    .setHTML("<h1>Nhà hàng Hà Nội</h1>")
    .addTo(map);

map.addControl(
    new mapboxgl.NavigationControl({
        showCompass: true,
        showZoom: true,
    })
);

map.addControl(
    new mapboxgl.GeolocateControl({
        positionOptions: {
            enableHighAccuracy: true,
        },
        trackUserLocation: true,
    })
);
map.on("dblclick", function (e) {
    new mapboxgl.Marker({
        color: "blue",
        draggable: true,
    })
        .setLngLat([e.lngLat.lng, e.lngLat.lat])
        .addTo(map);
});
map.addControl(
    new MapboxDirections({
        accessToken: mapboxgl.accessToken,
    }),
    "top-left"
);
var scale = new mapboxgl.ScaleControl({
    maxWidth: 80,
    unit: "imperial",
});
map.addControl(scale);
scale.setUnit("metric");
map.addControl(new mapboxgl.FullscreenControl());
var language = new MapboxLanguage({ defaultLanguage: "vi" });
map.addControl(language);