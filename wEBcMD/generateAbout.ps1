
$dir = Get-Location;
"Current location: $dir" | Out-Host;

$sha = (git rev-parse HEAD).substring(0,8)
"Commit: $sha" | Out-Host

$branch = git rev-parse --abbrev-ref HEAD
"Branch: $branch" | Out-Host

$infoHtml = @"
    <head>
		<style>
		.center {
			position: absolute;
			left: 50%;
			top: 50%;
			transform: translate(-50%, -50%);
			padding: 10px;
			border: 5px solid #000000;
		}
		</style>
	 </head>
    <body>
			<div class="center">
				<h1>Info</h1>
				<p>Branch: $branch.</p>
				<p>Commit: $sha.</p>
				<p><a href="javascript:history.back()">Go Back</a></p>
			</div>
    </body>
"@

Set-Content -Path ./wwwroot/info.html -Value $infoHtml
