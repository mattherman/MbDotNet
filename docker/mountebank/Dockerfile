FROM mhart/alpine-node

EXPOSE 2525
ENTRYPOINT ["mb"]
CMD ["start","--mock"]
ENV MOUNTEBANK_VERSION=1.9.0

RUN apk add --update nodejs
RUN npm install -g mountebank@${MOUNTEBANK_VERSION} --production