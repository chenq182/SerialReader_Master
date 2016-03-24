#!/usr/bin/python
# -*- coding: utf-8 -*-

from mod_python import apache

def index(req):
	req.log_error('handler')
	req.content_type = 'text/html'
	req.send_http_header()
	html = """
<html>
<head>
	<title>Testing mod_python</title>
</head>
	<body>
Hello World! - mod_python.publisher
	</body>
</html>
	"""
	req.write(html)
