msbuild .
nuget pack ApplicationAcquisitionSubmissions.Contract.nuspec -Version ${GO_PIPELINE_LABEL}
cp bin/Debug/ApplicationAcquisitionSubmissions.Contract.${GO_PIPELINE_LABEL}.nupkg .
nuget push ApplicationAcquisitionSubmissions.Contract.${GO_PIPELINE_LABEL}.nupkg  --api-key ${NEXUS_APIKEY} --source ${NEXUS_ADDRESS}
