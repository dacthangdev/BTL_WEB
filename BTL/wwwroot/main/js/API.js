
 //Hàm lấy Thêm mới Khách hàng. Dùng $.ajax() thực hiện method HTTP POST
	function insertDatBan() { 
	var url = 'https://localhost:7000/api/datbanapi?name=' +
	$('#name').val() + '&email=' + $('#email').val() + '&phone=' + $('#phone').val() + '&date=' + $('#book_date').val() + '&time='+ $('#book_time').val() + '&person=' + $('#soluong option:selected').text();
	$.ajax({
		url: url,
	method: 'POST',
	contentType: 'json',
	dataType: 'json',
	error: function (response) {
		alert("Đặt bàn thất bại");
		},
	success: function (reponse) {
		alert("Đặt bàn thành công");
		}
	});
 }