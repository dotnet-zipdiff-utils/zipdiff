require 'albacore'

desc "Build"
msbuild :build do |msb|
  msb.properties = { :configuration => :Release }
  msb.targets = [ :Clean, :Build ]
  msb.solution = "src/ZipDiff.sln"
end
