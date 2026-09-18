#!/usr/bin/env bash
#
# Publish this project's container image(s) to Docker Hub.
#
# Replaces the Windows build scripts that were run from the original build
# machine (build.bat / BuildCommands.ps1), which is no longer accessible.
#
# Environment overrides:
#   NAMESPACE   Docker Hub namespace           (default: klokedm)
#   TAG         image tag                      (default: latest)
#   PUSH        set to 0 to build without push (default: 1)
#
# Requires: docker login -u "$NAMESPACE"
#
set -euo pipefail

NAMESPACE="${NAMESPACE:-klokedm}"
TAG="${TAG:-latest}"
PUSH="${PUSH:-1}"

cd "$(dirname "${BASH_SOURCE[0]}")"

build_and_push() {
  local image="$1"; shift
  echo "==> building ${image}"
  docker build -t "${image}" "$@"
  if [[ "${PUSH}" == "1" ]]; then
    echo "==> pushing ${image}"
    docker push "${image}"
  else
    echo "==> PUSH=0 set, skipping push of ${image}"
  fi
}

# NOTE: the image name here is inferred from the Visual Studio publish profile
# (Properties/PublishProfiles/registry.hub.docker.com_klokedm.pubxml), which names
# the image after the lowercased project name. Confirm against your Docker Hub
# history before treating this as the canonical location.
build_and_push "${NAMESPACE}/dataexplorerapi:${TAG}" .

echo "done: ${NAMESPACE}/dataexplorerapi:${TAG}"
