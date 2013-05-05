require 'rubygems'
require 'albacore'
require 'version_bumper'

desc "Build"
msbuild :build => :assemblyinfo do |msb|
  msb.properties = { :configuration => :Release }
  msb.targets = [ :Clean, :Build ]
  msb.solution = 'src/ZipDiff.sln'
end

assemblyinfo :assemblyinfo do |asm|
  asm.version = bumper_version.to_s
  asm.file_version = bumper_version.to_s
  asm.output_file = 'src/SolutionInfo.cs'
end

output :output do |out|
  out.from '.'
  out.to 'out'
  out.file 'src/ZipDiff/bin/Release/zipdiff.exe', :as => 'zipdiff.exe'
end

zip :zip => :output do | zip |
  zip.directories_to_zip 'out'
  zip.output_file = "ZipDiff.#{bumper_version.to_s}.zip"
  zip.output_path = File.dirname(__FILE__)
end

task :nu => :output do
  `src/.nuget/NuGet.exe pack src/ZipDiff/ZipDiff.csproj`	
end

task :deploy => [:zip, :nu] do
end